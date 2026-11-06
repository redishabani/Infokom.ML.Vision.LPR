import argparse
import shutil
from pathlib import Path

from ultralytics import YOLO


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("mode", choices=["all"])
    parser.add_argument("data")
    parser.add_argument("epochs", type=int)
    parser.add_argument("onnx_output")
    args = parser.parse_args()

    data_yaml = str(Path(args.data).resolve())
    output = Path(args.onnx_output).resolve()
    output.parent.mkdir(parents=True, exist_ok=True)

    # Nano model is intentionally selected for CPU-friendly experimentation.
    model = YOLO("yolo11n.pt")

    model.train(
        data=data_yaml,
        epochs=args.epochs,
        imgsz=640,
        batch=2,
        device="cpu",
        workers=0,
        project=str(output.parent / "training-runs"),
        name="vehicle",
        exist_ok=True,
        patience=15,
        cache=False,
        plots=True,
        pretrained=True,
    )

    best_weights = (output.parent / "training-runs" / "vehicle" / "weights" / "best.pt").resolve()
    if not best_weights.exists():
        raise FileNotFoundError(f"Training did not produce {best_weights}")

    # Evaluate on the held-out test split, not the training split.
    best = YOLO(str(best_weights))
    metrics = best.val(
        data=data_yaml,
        split="test",
        imgsz=640,
        batch=1,
        device="cpu",
        workers=0,
        plots=True,
    )

    print("\nTEST METRICS")
    print(f"mAP50:     {metrics.box.map50:.4f}")
    print(f"mAP50-95:  {metrics.box.map:.4f}")
    print(f"Precision: {metrics.box.mp:.4f}")
    print(f"Recall:    {metrics.box.mr:.4f}")

    # Embedded NMS makes the C# output decoder simple: [1, N, 6].
    exported = best.export(
        format="onnx",
        imgsz=640,
        opset=17,
        nms=True,
        dynamic=False,
        simplify=False,
    )
    shutil.copy2(str(exported), output)
    print(f"\nONNX model written to: {output}")


if __name__ == "__main__":
    main()
