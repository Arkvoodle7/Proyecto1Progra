namespace SimuladorOtroBanco
{
    partial class frmSimuladorOtroBanco
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTelefono = new TextBox();
            txtMonto = new TextBox();
            txtDescripcion = new TextBox();
            btnEnviar = new Button();
            lblTelefono = new Label();
            lblMonto = new Label();
            lblDescripcion = new Label();
            SuspendLayout();
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(103, 37);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(100, 23);
            txtTelefono.TabIndex = 0;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(103, 95);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(100, 23);
            txtMonto.TabIndex = 1;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(103, 155);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(100, 23);
            txtDescripcion.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(139, 236);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(75, 23);
            btnEnviar.TabIndex = 3;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(12, 37);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(52, 15);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono";
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(12, 95);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(43, 15);
            lblMonto.TabIndex = 5;
            lblMonto.Text = "Monto";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(12, 158);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // frmSimuladorOtroBanco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(231, 281);
            Controls.Add(lblDescripcion);
            Controls.Add(lblMonto);
            Controls.Add(lblTelefono);
            Controls.Add(btnEnviar);
            Controls.Add(txtDescripcion);
            Controls.Add(txtMonto);
            Controls.Add(txtTelefono);
            Name = "frmSimuladorOtroBanco";
            Text = "Simulador otro banco ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTelefono;
        private TextBox txtMonto;
        private TextBox txtDescripcion;
        private Button btnEnviar;
        private Label lblTelefono;
        private Label lblMonto;
        private Label lblDescripcion;
    }
}
