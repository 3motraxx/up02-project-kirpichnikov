from pathlib import Path
import tkinter as tk
from config import PATH_PICTURE, PATH_LOGO, PATH_ICON, RESOURCES_DIR

def load_image(path, size=None):
    path = Path(path)
    if not path.is_absolute():
        path = RESOURCES_DIR / path
    if not path.exists():
        return None
    try:
        image = tk.PhotoImage(file=str(path))
        if size and size[0] > 0 and size[1] > 0:
            factor = max(1, (image.width() + size[0] - 1) // size[0],
                         (image.height() + size[1] - 1) // size[1])
            if factor > 1:
                image = image.subsample(factor, factor)
        return image
    except Exception:
        try:
            from PIL import Image, ImageTk
            image = Image.open(path)
            image.thumbnail(size or (500, 500))
            return ImageTk.PhotoImage(image)
        except Exception:
            return None
