# VehicleDetectionDemo

A local, CPU-only, one-class vehicle object-detection example.

- C#/.NET: dataset preparation, training orchestration, ONNX Runtime inference.
- Python: Ultralytics YOLO11 training/evaluation/ONNX export.
- Class 0: `vehicle`.

## Requirements

- .NET 8 SDK
- Python 3.10+ (use a Python version supported by the installed PyTorch/Ultralytics release)
- Internet for the first setup and initial `yolo11n.pt` pretrained-weight download.
- CPU; training is intentionally configured for CPU and will be slow compared with a GPU.

## Setup

From this directory:

```powershell
dotnet restore .\VehicleDetectionDemo\VehicleDetectionDemo.csproj
py -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install --upgrade pip
pip install -r .\python\requirements.txt
```

If `python` is not on PATH, set `PYTHON` to the full interpreter path.

## Annotation JSON

See `data/annotations.example.json`.

- `file` is relative to the JSON file's directory.
- `x`, `y`, `width`, `height` are pixel coordinates, origin at top-left.
- Each image can contain multiple vehicles.
- Use `"objects": []` for negative images containing no vehicles.
- At least 10 images are required by this demo.

## Run

```powershell
dotnet run --project .\VehicleDetectionDemo -- prepare .\data\annotations.json .\data\vehicle-dataset
dotnet run --project .\VehicleDetectionDemo -- train .\data\vehicle-dataset 50
dotnet run --project .\VehicleDetectionDemo -- infer .\data\vehicle-dataset\artifacts\vehicle.onnx .\data\test.jpg
```

The training command performs:
1. Fine-tuning from `yolo11n.pt` on CPU.
2. Evaluation on the held-out `test` split.
3. ONNX export with embedded NMS.
4. Copy of the exported model to `artifacts/vehicle.onnx`.
5. Saving training runs and checkpoints under `artifacts/training-runs/vehicle/`.

## Output

The C# inference output contains `vehicle`, confidence, and bounding rectangle in original-image pixel coordinates.

The inference code expects the NMS-enabled YOLO11 export output `[1, N, 6]`:
`x1, y1, x2, y2, confidence, class_id`.

## Important dataset notes

- Do not put near-identical frames from the same video sequence in both train and test splits; that leaks scene information and inflates evaluation.
- Include varied weather, lighting, viewpoints, vehicle sizes, occlusions and empty-road images.
- A single `vehicle` class detects vehicles but does not distinguish cars, trucks, buses or motorcycles.
- The random split in this demo is image-level. For video data, replace it with a sequence/camera/time-based split.
- Review labels visually before training. Incorrect boxes directly teach incorrect localization.

## CPU expectations

Start with 20–50 epochs, image size 640 and batch size 2. CPU training can take hours or longer depending on image count and processor. For a smoke test, use 3 epochs on a small dataset; that is not a production-quality model.

## Licensing

Ultralytics is offered under AGPL-3.0 and an Enterprise license. Review the current terms before integrating the training code or resulting model into a proprietary or commercial product:
https://www.ultralytics.com/license

ONNX Runtime and ImageSharp have their own licenses; review all dependency terms for your distribution.
