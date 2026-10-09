import tkinter as tk
from tkinter import ttk, messagebox
from config import APP_TITLE, PATH_LOGO, PATH_ICON
from styles import COLOR_MAIN_BG, COLOR_SECONDARY_BG, FONT_SIZE_TITLE, font
from resources import load_image
import database as db
from catalog import create_product_card

class CatalogWindow:
    def __init__(self):
        self.root = tk.Tk()
        self.root.title(APP_TITLE)
        self.root.geometry("1000x740")
        self.root.minsize(760, 550)
        self.root.configure(bg=COLOR_MAIN_BG)
        try:
            if PATH_ICON.exists():
                self.root.iconbitmap(str(PATH_ICON))
        except Exception:
            pass
        self.build_ui()
        self.load_products()

    def build_ui(self):
        header = tk.Frame(self.root, bg=COLOR_SECONDARY_BG, height=82)
        header.pack(fill="x")
        header.pack_propagate(False)
        logo = load_image(PATH_LOGO, (60, 60))
        if logo:
            label = tk.Label(header, image=logo, bg=COLOR_SECONDARY_BG)
            label.image = logo
            label.pack(side="left", padx=15, pady=8)
        tk.Label(header, text="КАТАЛОГ ТОВАРОВ", bg=COLOR_SECONDARY_BG,
                 font=font(FONT_SIZE_TITLE, True)).pack(side="left", padx=20)

        toolbar = tk.Frame(self.root, bg=COLOR_MAIN_BG, padx=12, pady=8)
        toolbar.pack(fill="x")
        tk.Button(toolbar, text="Обновить каталог",
                  command=self.refresh_catalog).pack(side="left")
        tk.Button(toolbar, text="Заказы", command=self.open_orders).pack(side="left", padx=8)

        area = tk.Frame(self.root, bg=COLOR_MAIN_BG)
        area.pack(fill="both", expand=True)
        self.canvas = tk.Canvas(area, bg=COLOR_MAIN_BG, highlightthickness=0)
        scrollbar = ttk.Scrollbar(area, orient="vertical", command=self.canvas.yview)
        self.catalog_frame = tk.Frame(self.canvas, bg=COLOR_MAIN_BG)
        self._frame_window = self.canvas.create_window((0, 0), window=self.catalog_frame, anchor="nw")
        self.catalog_frame.bind("<Configure>",
            lambda _e: self.canvas.configure(scrollregion=self.canvas.bbox("all")))
        self.canvas.bind("<Configure>",
            lambda e: self.canvas.itemconfigure(self._frame_window, width=e.width))
        self.canvas.configure(yscrollcommand=scrollbar.set)
        self.canvas.pack(side="left", fill="both", expand=True)
        scrollbar.pack(side="right", fill="y")

    def load_products(self):
        try:
            products = db.get_all_products()
            if not products:
                tk.Label(self.catalog_frame, text="В базе пока нет товаров.",
                         bg=COLOR_MAIN_BG).pack(pady=30)
                return
            for product in products:
                create_product_card(self.catalog_frame, product, refresh=self.refresh_catalog)
        except Exception as exc:
            messagebox.showerror("Ошибка загрузки каталога", str(exc), parent=self.root)

    def refresh_catalog(self):
        for widget in self.catalog_frame.winfo_children():
            widget.destroy()
        self.load_products()

    def open_orders(self):
        from orders_window import OrdersWindow
        OrdersWindow(self.root)

    def run(self):
        self.root.mainloop()

if __name__ == "__main__":
    CatalogWindow().run()
