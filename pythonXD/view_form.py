import tkinter as tk
from tkinter import messagebox
from styles import COLOR_MAIN_BG, COLOR_SECONDARY_BG, COLOR_ACCENT, COLOR_TEXT, FONT_SIZE_NORMAL, FONT_SIZE_TITLE, font
from resources import load_image, PATH_PICTURE
from order_manager import add_order_to_db, get_product_quantity
from error_handler import validate_positive_int
from config import DEFAULT_CLIENT

def _value(product, *names, default=""):
    for name in names:
        if name in product and product[name] is not None:
            return product[name]
    return default

class ViewForm:
    def __init__(self, parent, product, on_add_to_order=None):
        self.product = product
        self.on_add_to_order = on_add_to_order
        self.window = tk.Toplevel(parent)
        self.window.title(f"Просмотр — {_value(product, 'название', default='Товар')}")
        self.window.geometry("720x560")
        self.window.minsize(650, 500)
        self.window.configure(bg=COLOR_MAIN_BG)
        self.window.transient(parent)
        self.build_ui()

    def build_ui(self):
        header = tk.Frame(self.window, bg=COLOR_SECONDARY_BG, height=60)
        header.pack(fill="x")
        header.pack_propagate(False)
        tk.Label(header, text="КАРТОЧКА ТОВАРА", bg=COLOR_SECONDARY_BG,
                 font=font(FONT_SIZE_TITLE, True)).pack(expand=True)

        body = tk.Frame(self.window, bg=COLOR_MAIN_BG, padx=18, pady=18)
        body.pack(fill="both", expand=True)

        image_frame = tk.Frame(body, bg=COLOR_MAIN_BG, width=230, height=270)
        image_frame.pack(side="left", fill="y", padx=(0, 18))
        image_frame.pack_propagate(False)
        image_path = _value(self.product, "фото", default="")
        image = load_image(image_path, (210, 220)) if image_path else None
        if image is None:
            image = load_image(PATH_PICTURE, (210, 220))
        if image:
            label = tk.Label(image_frame, image=image, bg=COLOR_MAIN_BG)
            label.image = image
            label.pack(expand=True)
        else:
            tk.Label(image_frame, text="Нет изображения", bg=COLOR_MAIN_BG).pack(expand=True)

        info = tk.Frame(body, bg=COLOR_MAIN_BG)
        info.pack(side="left", fill="both", expand=True)

        fields = [
            ("Название", _value(self.product, "название", default="—")),
            ("Категория", _value(self.product, "категория", default="—")),
            ("Состав", _value(self.product, "состав", default="Не указано")),
            ("Цена", f"{float(_value(self.product, 'цена', default=0) or 0):.2f} руб."),
            ("Остаток", _value(self.product, "количество", default=0)),
        ]
        for label, value in fields:
            row = tk.Frame(info, bg=COLOR_MAIN_BG)
            row.pack(fill="x", pady=5)
            tk.Label(row, text=f"{label}:", width=16, anchor="w",
                     bg=COLOR_MAIN_BG, font=font(FONT_SIZE_NORMAL, True)).pack(side="left")
            tk.Label(row, text=str(value), anchor="w", justify="left",
                     wraplength=300, bg=COLOR_MAIN_BG,
                     font=font(FONT_SIZE_NORMAL)).pack(side="left", fill="x", expand=True)

        bottom = tk.Frame(self.window, bg=COLOR_MAIN_BG, padx=18, pady=12)
        bottom.pack(fill="x")
        tk.Label(bottom, text="Количество:", bg=COLOR_MAIN_BG,
                 font=font(FONT_SIZE_NORMAL, True)).pack(side="left")
        self.quantity_var = tk.StringVar(value="1")
        tk.Entry(bottom, textvariable=self.quantity_var, width=7,
                 font=font(FONT_SIZE_NORMAL)).pack(side="left", padx=8)
        tk.Button(bottom, text="Добавить в заказ", command=self.add_to_order,
                  bg=COLOR_ACCENT, fg="white", font=font(FONT_SIZE_NORMAL, True),
                  padx=10, pady=7).pack(side="left", padx=8)
        tk.Button(bottom, text="Закрыть", command=self.window.destroy,
                  font=font(FONT_SIZE_NORMAL), padx=10, pady=7).pack(side="right")

    def add_to_order(self):
        if not self.product:
            messagebox.showerror("Ошибка", "Товар не выбран.", parent=self.window)
            return
        valid, quantity, error = validate_positive_int(self.quantity_var.get())
        if not valid:
            messagebox.showwarning("Проверьте количество", error, parent=self.window)
            return
        if quantity > 10:
            messagebox.showwarning(
                "Проверьте количество",
                "Можно заказать не более 10 штук за раз.",
                parent=self.window
            )
            return
        product_id = int(_value(self.product, "id", default=0))

        if product_id <= 0:
            messagebox.showerror(
                "Ошибка",
                "Не удалось определить товар.",
                parent=self.window
            )
            return
        try:
            current_qty = get_product_quantity(product_id)
            if current_qty < quantity:
                messagebox.showwarning("Товар закончился",
                    f"На складе осталось: {current_qty}.", parent=self.window)
                return
            order_id = add_order_to_db(DEFAULT_CLIENT, product_id, quantity)
            messagebox.showinfo("Успех",
                f"Заказ №{order_id} оформлен.\nКоличество: {quantity}.",
                parent=self.window)
            if self.on_add_to_order:
                self.on_add_to_order()
            self.window.destroy()
        except Exception as exc:
            messagebox.showerror("Ошибка заказа", str(exc), parent=self.window)
