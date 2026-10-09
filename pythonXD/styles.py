import tkinter as tk

COLOR_MAIN_BG = "#ffffff"
COLOR_SECONDARY_BG = "#d2f6e7"
COLOR_ACCENT = "#70b2af"
COLOR_HIGHLIGHT = "#ff8080"
COLOR_TEXT = "#222222"

FONT_FAMILY = "Calibri"
FONT_SIZE_SMALL = 10
FONT_SIZE_NORMAL = 12
FONT_SIZE_HEADER = 14
FONT_SIZE_TITLE = 18

def font(size=FONT_SIZE_NORMAL, bold=False):
    return (FONT_FAMILY, size, "bold" if bold else "normal")
