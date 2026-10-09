import tkinter as tk
from styles import (
    COLOR_MAIN_BG, COLOR_HIGHLIGHT, COLOR_TEXT,
    FONT_SIZE_NORMAL, FONT_SIZE_HEADER, font
)
from resources import load_image, PATH_PICTURE


def _value(product, *names, default=""):
    for name in names:
        if name in product and product[name] is not None:
            return product[name]
    return default


def _get_card_color(quantity):
    return COLOR_HIGHLIGHT if int(quantity or 0) <= 3 else COLOR_MAIN_BG


def _open_view(parent, product, refresh=None):
    from view_form import ViewForm
    ViewForm(parent, product, on_add_to_order=refresh)


def create_product_card(parent, product, refresh=None):
    qty = int(_value(product, "количество", default=0) or 0)
    bg = _get_card_color(qty)

    card = tk.Frame(parent, bg=bg, bd=1, relief="solid", height=150,
                    padx=12, pady=10)
    card.pack(fill="x", padx=10, pady=6)
    card.pack_propagate(False)

    image_frame = tk.Frame(card, bg=bg, width=115, height=125)
    image_frame.pack(side="left", padx=(0, 14))
    image_frame.pack_propagate(False)
    image_path = _value(product, "фото", default="")
    image = load_image(image_path, (105, 105)) if image_path else None
    if image is None:
        image = load_image(PATH_PICTURE, (105, 105))
    if image:
        photo = tk.Label(image_frame, image=image, bg=bg)
        photo.image = image
        photo.pack(expand=True)
    else:
        tk.Label(image_frame, text="Нет фото", bg=bg, fg=COLOR_TEXT).pack(expand=True)

    details = tk.Frame(card, bg=bg)
    details.pack(side="left", fill="both", expand=True)
    name = _value(product, "название", default="Без названия")
    category = _value(product, "категория", default="—")
    composition = _value(product, "состав", default="—")
    price = _value(product, "цена", default=0)

    tk.Label(details, text=str(name), bg=bg, fg=COLOR_TEXT,
             font=font(FONT_SIZE_HEADER, True), anchor="w").pack(fill="x")
    tk.Label(details, text=f"Категория: {category}", bg=bg,
             font=font(FONT_SIZE_NORMAL), anchor="w").pack(fill="x", pady=(5, 0))
    tk.Label(details, text=f"Состав: {composition}", bg=bg,
             font=font(FONT_SIZE_NORMAL), anchor="w").pack(fill="x", pady=(3, 0))
    tk.Label(details, text=f"Количество: {qty}", bg=bg,
             font=font(FONT_SIZE_NORMAL), anchor="w").pack(fill="x", pady=(3, 0))
    tk.Label(card, text=f"{float(price or 0):.2f} руб.", bg=bg,
             font=font(FONT_SIZE_HEADER, True), anchor="e").pack(side="right", padx=8)

    def open_card(_event=None):
        _open_view(parent.winfo_toplevel(), product, refresh)

    for widget in (card, image_frame, details, *card.winfo_children(),
                   *details.winfo_children(), *image_frame.winfo_children()):
        widget.bind("<Button-1>", open_card)
        widget.configure(cursor="hand2")
    return card
