namespace ShoeStoreApp
{
    partial class OrdersForm
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
            buttonAddOrder = new Button();
            flowOrders = new FlowLayoutPanel();
            buttonBack = new Button();
            labelUser = new Label();
            SuspendLayout();
            // 
            // buttonAddOrder
            // 
            buttonAddOrder.BackColor = Color.MediumSpringGreen;
            buttonAddOrder.FlatStyle = FlatStyle.Flat;
            buttonAddOrder.Location = new Point(12, 12);
            buttonAddOrder.Name = "buttonAddOrder";
            buttonAddOrder.Size = new Size(159, 59);
            buttonAddOrder.TabIndex = 0;
            buttonAddOrder.Text = "Добавить заказ";
            buttonAddOrder.UseVisualStyleBackColor = false;
            buttonAddOrder.Click += buttonAddOrder_Click;
            // 
            // flowOrders
            // 
            flowOrders.AutoScroll = true;
            flowOrders.BorderStyle = BorderStyle.FixedSingle;
            flowOrders.FlowDirection = FlowDirection.TopDown;
            flowOrders.Location = new Point(12, 77);
            flowOrders.Name = "flowOrders";
            flowOrders.Size = new Size(1042, 534);
            flowOrders.TabIndex = 1;
            flowOrders.WrapContents = false;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.MediumSpringGreen;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Location = new Point(450, 617);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(147, 58);
            buttonBack.TabIndex = 2;
            buttonBack.Text = "Назад";
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += buttonBack_Click;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Location = new Point(614, 27);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(131, 22);
            labelUser.TabIndex = 3;
            labelUser.Text = "Пользователь:";
            // 
            // OrdersForm
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 688);
            Controls.Add(labelUser);
            Controls.Add(buttonBack);
            Controls.Add(flowOrders);
            Controls.Add(buttonAddOrder);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
            Name = "OrdersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Список заказов";
            Load += OrdersForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAddOrder;
        private FlowLayoutPanel flowOrders;
        private Button buttonBack;
        private Label labelUser;
    }
}