namespace ShoeStoreApp
{
    partial class ProductsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelUser = new Label();
            buttonExit = new Button();
            flowProducts = new FlowLayoutPanel();
            buttonAddProduct = new Button();
            SuspendLayout();
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelUser.Location = new Point(688, 8);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(125, 22);
            labelUser.TabIndex = 0;
            labelUser.Text = "Пользователь";
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.MediumSpringGreen;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonExit.Location = new Point(641, 572);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(321, 35);
            buttonExit.TabIndex = 2;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // flowProducts
            // 
            flowProducts.AutoScroll = true;
            flowProducts.Location = new Point(12, 33);
            flowProducts.Name = "flowProducts";
            flowProducts.Size = new Size(950, 533);
            flowProducts.TabIndex = 3;
            // 
            // buttonAddProduct
            // 
            buttonAddProduct.BackColor = Color.MediumSpringGreen;
            buttonAddProduct.FlatStyle = FlatStyle.Flat;
            buttonAddProduct.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonAddProduct.Location = new Point(12, 1);
            buttonAddProduct.Name = "buttonAddProduct";
            buttonAddProduct.Size = new Size(177, 29);
            buttonAddProduct.TabIndex = 4;
            buttonAddProduct.Text = "Добавить товар";
            buttonAddProduct.UseVisualStyleBackColor = false;
            buttonAddProduct.Click += buttonAddProduct_Click_1;
            // 
            // ProductsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1054, 619);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonExit);
            Controls.Add(flowProducts);
            Controls.Add(labelUser);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "ProductsForm";
            Text = "Список товаров";
            Load += ProductsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUser;
        private Button buttonExit;
        private FlowLayoutPanel flowProducts;
        private Button buttonAddProduct;
    }
}