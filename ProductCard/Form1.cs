using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProductCard
{
    public partial class Form1 : Form
    {
        private FlowLayoutPanel catalogPanel;

        public Form1()
        {
            InitializeComponent();

            BuildUI();
            LoadProducts();
        }

        private void BuildUI()
        {
            Text = "Каталог товаров";
            Width = 900;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            string iconPath = Resources.PATH_ICON;

            if (System.IO.File.Exists(iconPath))
            {
                try
                {
                    Icon = new Icon(iconPath);
                }
                catch
                {

                }
            }

            Panel header = new Panel();

            header.Dock = DockStyle.Top;
            header.Height = 80;
            header.BackColor = Styles.COLOR_SECONDARY_BG;


            PictureBox logo = new PictureBox();

            logo.Width = 60;
            logo.Height = 60;
            logo.Location = new Point(15, 10);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.BackColor = Color.Transparent;

            Image? logoImage = Resources.LoadImageProportional(
                Resources.PATH_LOGO,
                new Size(60, 60)
            );

            if (logoImage != null)
            {
                logo.Image = logoImage;
            }

            header.Controls.Add(logo);

            Label title = new Label();

            title.Text = "КАТАЛОГ ТОВАРОВ";
            title.Font = Styles.Font(
            Styles.FONT_SIZE_TITLE,
             true
            );
            title.Dock = DockStyle.Fill;
            title.TextAlign = ContentAlignment.MiddleCenter;

            header.Controls.Add(title);

            catalogPanel = new FlowLayoutPanel();

            catalogPanel.Dock = DockStyle.Fill;
            catalogPanel.BackColor = Color.White;
            catalogPanel.FlowDirection = FlowDirection.TopDown;
            catalogPanel.WrapContents = false;
            catalogPanel.AutoScroll = true;
            catalogPanel.Padding = new Padding(10);

            Controls.Add(catalogPanel);
            Controls.Add(header);
        }

        private void LoadProducts()
        {
            List<Product> products =
                Database.GetAllProducts();

            foreach (Product product in products)
            {
                Panel card = CreateProductCard(product);

                catalogPanel.Controls.Add(card);
            }
        }

        private Panel CreateProductCard(Product product)
        {
            Color bgColor =
     product.Quantity <= 3
         ? Styles.COLOR_HIGHLIGHT
         : Styles.COLOR_MAIN_BG;

            Panel card = new Panel();

            card.Width = 820;
            card.Height = 160;
            card.BackColor = bgColor;
            card.BorderStyle =
                BorderStyle.FixedSingle;
            card.Margin = new Padding(5);
            card.BorderStyle = BorderStyle.FixedSingle;

            PictureBox picture = new PictureBox();

            picture.Width = 100;
            picture.Height = 100;
            picture.Location = new Point(15, 25);
            picture.BackColor = Color.LightGray;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            string imagePath = product.ImagePath;

            bool isPlaceholder =
                string.IsNullOrWhiteSpace(imagePath) ||
                !System.IO.File.Exists(imagePath);

            if (isPlaceholder)
            {
                picture.Image = Resources.GetProductImage(
                    null,
                    new Size(100, 100)
                );

                Label noPhoto = new Label();

                noPhoto.Text = "Нет фото";
                noPhoto.Font = Styles.Font(
                    Styles.FONT_SIZE_SMALL,
                    true
                );
                noPhoto.BackColor = Color.Transparent;
                noPhoto.AutoSize = true;
                noPhoto.Location = new Point(25, 65);

                picture.Controls.Add(noPhoto);
            }
            else
            {
                picture.Image = Resources.GetProductImage(
                    imagePath,
                    new Size(100, 100)
                );
            }


            Label title = new Label();

            title.Text =
                $"Цветы | {product.Name}";

            title.Font = new Font(
                "Arial",
                14,
                FontStyle.Bold
            );

            title.Location =
                new Point(135, 15);
            title.AutoSize = true;

            Label category = new Label();

            category.Text =
                $"Категория: {product.Category}";

            category.Font =
                new Font("Arial", 11);

            category.Location =
                new Point(135, 50);
            category.AutoSize = true;

            string indicator =
                product.Quantity > 5
                    ? "много"
                    : "мало";

            Label quantity = new Label();

            quantity.Text =
                $"Количество: {indicator} ({product.Quantity})";

            quantity.Font =
                new Font("Arial", 11);

            quantity.Location =
                new Point(135, 75);
            quantity.AutoSize = true;

            Label composition = new Label();

            composition.Text =
                $"Состав: {product.Composition}";

            composition.Font =
                new Font("Arial", 11);

            composition.Location =
                new Point(135, 100);
            composition.AutoSize = true;

            Label price = new Label();

            price.Text =
                $"{product.Price:F2} руб.";

            price.Font = new Font(
                "Arial",
                14,
                FontStyle.Bold
            );

            price.Location =
                new Point(650, 60);
            price.AutoSize = true;

            card.Controls.Add(picture);
            card.Controls.Add(title);
            card.Controls.Add(category);
            card.Controls.Add(quantity);
            card.Controls.Add(composition);
            card.Controls.Add(price);

            return card;
        }
    }
}