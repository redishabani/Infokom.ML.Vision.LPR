using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Text;

namespace Infokom.ML.Models
{
	

	









	public interface ISensor<TSensation>
	{
		
	}
	
	public interface IPerceptor<TPerceptor, TSensation, TPerception> where TPerceptor : IPerceptor<TPerceptor, TSensation, TPerception>
	{





		public static abstract TPerceptor Create<TModel>(TModel model) where TModel : IModel;
	}



	public interface IModel
	{
		public ModelInfo GetInfo();
	}



	public record ModelInfo
	{
		/// <summary>
		/// Author/Creator (e.g. ultralytics.com)
		/// </summary>
		public string Provider { get; init; }

		/// <summary>
		/// The core model name or lineage (e.g. YOLO)
		/// </summary>
		public required string Family { get; init; }

		/// <summary>
		/// Semantic versioning showing updates or iterations (e.g. small version 8 YOLO: yolov8)
		/// </summary>
		public required string Version { get; init; }

		/// <summary>
		/// Capability/Purpose fo the model (e.g. small version 8 YOLO for segmenation: yolov8s-seg  )
		/// </summary>
		public string Variant { get; init; }

		/// <summary>
		/// Packing/File format model data (e.g. ONNX small version 8 YOLO for segmenation: yolov8s-seg.onnx)
		/// </summary>
		public required string Format { get; init; }
	}

	public static class Model
	{
		private const string MODELS_ROOT = @"%USERPROFILE%\AppData\Local\Infokom\ML\Models\";


		public class ModelFile : IModel
		{
			private readonly FileInfo _file;

			public ModelFile(FileInfo file) => this._file = file;

			public ModelInfo GetInfo()
			{
				var model = _file;
				var version = model.Directory;
				var family = version.Parent;
				var provider = family.Parent;
				var format = model.Extension[1..];
				var name = model.Name[..(model.Name.Length - format.Length - 1)];
				var variant = name.Split('-')[1];

				return new ModelInfo()
				{
					Provider = provider.Name,
					Family = family.Name,
					Version = version.Name,
					Variant = variant,
					Format = format
				};
			}
		}

		private class ProviderDirectory : IGrouping<string, IModel>
		{
			private readonly DirectoryInfo _directory;

			public ProviderDirectory(DirectoryInfo directory)
			{
				_directory = directory;
			}

			public string Name => _directory.Name;

			string IGrouping<string, IModel>.Key => this.Name;
			IEnumerator<IModel> IEnumerable<IModel>.GetEnumerator() => throw new NotImplementedException();
			IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
		}

		private class ModelDirectory : ILookup<string, IModel>
		{
			private const string ROOT = @"\AppData\Local\Infokom\ML\Models\";//%USERPROFILE%

			private readonly DirectoryInfo _root;

			public ModelDirectory() => _root = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ROOT));


			public IEnumerable<IModel> this[string key] => _root
				.EnumerateDirectories(key)
				.SingleOrDefault() is DirectoryInfo directory ? new ProviderDirectory(directory) : null;

			int ILookup<string, IModel>.Count => _root.EnumerateDirectories().Count();

			bool ILookup<string, IModel>.Contains(string key) => _root
				.EnumerateDirectories(key)
				.Any();

			public IEnumerator<IGrouping<string, IModel>> GetEnumerator() => _root
				.EnumerateDirectories()
				.Select(d => new ProviderDirectory(d))
				.GetEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		}



		public static ILookup<string, IModel> Providers { get; } = new ModelDirectory();







		private class RootNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

			public IEnumerable<ProviderNode> Providers => _dir.EnumerateDirectories().Select(d => new ProviderNode(d));
		}

		private class ProviderNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

			public IEnumerable<FamilyNode> Families => _dir.EnumerateDirectories().Select(d => new FamilyNode(d));
		}

		private class FamilyNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

			public IEnumerable<VersionNode> Versions => _dir.EnumerateDirectories().Select(d => new VersionNode(d));

		}

		private class VersionNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

			public IEnumerable<SizeNode> Versions => _dir.EnumerateDirectories().Select(d => new SizeNode(d));

		}

		private class SizeNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

			public IEnumerable<FormatNode> Formats => _dir.EnumerateDirectories().Select(d => new FormatNode(d));

		}

		private class FormatNode(DirectoryInfo dir)
		{
			private readonly DirectoryInfo _dir = dir;

		}
	}	
}
