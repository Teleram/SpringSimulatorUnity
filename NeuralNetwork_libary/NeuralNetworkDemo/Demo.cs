using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace NeuralNetworkDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Demo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button b_clear_pts;
		private System.Windows.Forms.Button b_clear_draw;
		private System.Windows.Forms.Button b_learn;
		private System.Windows.Forms.Button b_draw_out;
		private NeuralNetworkDemo.Demo2D_Draw demo2D_Draw1;
		private NeuralNetworkGUI.NeuralNetworkEditor neuralNetworkEditor1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel3;
		private NeuralNetworkGUI.NeuralNetworkEditor neuralNetworkEditor2;
		private NeuralNetworkDemo.Demo3D_draw demo3D_draw1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button b_3d_drawnn;
		private System.Windows.Forms.Button b_3d_clearnn;
		private System.Windows.Forms.Button b_3d_learn;
		private System.Windows.Forms.Button b_3d_cpts;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown num_res;

		int[] defaultNN = {6,4,1};

		public Demo()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			neuralNetworkEditor1.Neural_Network = new NeuralNetwork.NeuralNetwork(1,defaultNN);
			demo2D_Draw1.setNeuralNetwork(neuralNetworkEditor1.Neural_Network);
			neuralNetworkEditor1.lockNetworkIO(1,1);
			neuralNetworkEditor2.Neural_Network = new NeuralNetwork.NeuralNetwork(2,defaultNN);
			neuralNetworkEditor2.lockNetworkIO(2,1);
			demo3D_draw1.setNeuralNetwork(neuralNetworkEditor2.Neural_Network);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.b_clear_pts = new System.Windows.Forms.Button();
			this.b_clear_draw = new System.Windows.Forms.Button();
			this.b_learn = new System.Windows.Forms.Button();
			this.b_draw_out = new System.Windows.Forms.Button();
			this.demo2D_Draw1 = new NeuralNetworkDemo.Demo2D_Draw();
			this.neuralNetworkEditor1 = new NeuralNetworkGUI.NeuralNetworkEditor();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.num_res = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.demo3D_draw1 = new NeuralNetworkDemo.Demo3D_draw();
			this.b_3d_drawnn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.b_3d_clearnn = new System.Windows.Forms.Button();
			this.b_3d_learn = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.neuralNetworkEditor2 = new NeuralNetworkGUI.NeuralNetworkEditor();
			this.b_3d_cpts = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_res)).BeginInit();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// b_clear_pts
			// 
			this.b_clear_pts.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.b_clear_pts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_clear_pts.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_clear_pts.Location = new System.Drawing.Point(56, 504);
			this.b_clear_pts.Name = "b_clear_pts";
			this.b_clear_pts.Size = new System.Drawing.Size(136, 23);
			this.b_clear_pts.TabIndex = 0;
			this.b_clear_pts.Text = "Clear points";
			this.b_clear_pts.Click += new System.EventHandler(this.b_clear_pts_Click);
			// 
			// b_clear_draw
			// 
			this.b_clear_draw.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.b_clear_draw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_clear_draw.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_clear_draw.Location = new System.Drawing.Point(336, 488);
			this.b_clear_draw.Name = "b_clear_draw";
			this.b_clear_draw.Size = new System.Drawing.Size(240, 23);
			this.b_clear_draw.TabIndex = 1;
			this.b_clear_draw.Text = "Clear neural network output";
			this.b_clear_draw.Click += new System.EventHandler(this.b_clear_draw_Click);
			// 
			// b_learn
			// 
			this.b_learn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.b_learn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_learn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_learn.Location = new System.Drawing.Point(208, 504);
			this.b_learn.Name = "b_learn";
			this.b_learn.Size = new System.Drawing.Size(104, 23);
			this.b_learn.TabIndex = 2;
			this.b_learn.Text = "Learn";
			this.b_learn.Click += new System.EventHandler(this.b_learn_Click);
			// 
			// b_draw_out
			// 
			this.b_draw_out.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.b_draw_out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_draw_out.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_draw_out.Location = new System.Drawing.Point(336, 520);
			this.b_draw_out.Name = "b_draw_out";
			this.b_draw_out.Size = new System.Drawing.Size(240, 23);
			this.b_draw_out.TabIndex = 3;
			this.b_draw_out.Text = "Draw neural network output";
			this.b_draw_out.Click += new System.EventHandler(this.b_draw_out_Click);
			// 
			// demo2D_Draw1
			// 
			this.demo2D_Draw1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.demo2D_Draw1.Name = "demo2D_Draw1";
			this.demo2D_Draw1.Size = new System.Drawing.Size(600, 400);
			this.demo2D_Draw1.TabIndex = 4;
			// 
			// neuralNetworkEditor1
			// 
			this.neuralNetworkEditor1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.neuralNetworkEditor1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.neuralNetworkEditor1.Name = "neuralNetworkEditor1";
			this.neuralNetworkEditor1.Neural_Network = null;
			this.neuralNetworkEditor1.Size = new System.Drawing.Size(336, 552);
			this.neuralNetworkEditor1.TabIndex = 5;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.neuralNetworkEditor1});
			this.panel1.Location = new System.Drawing.Point(632, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(336, 552);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(592, 32);
			this.label1.TabIndex = 7;
			this.label1.Text = "Demo of function aproximation by a neural network";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.demo2D_Draw1});
			this.panel2.Location = new System.Drawing.Point(16, 72);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(600, 400);
			this.panel2.TabIndex = 8;
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(992, 608);
			this.tabControl1.TabIndex = 9;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.b_draw_out,
																				   this.label1,
																				   this.b_clear_draw,
																				   this.b_learn,
																				   this.panel1,
																				   this.panel2,
																				   this.b_clear_pts});
			this.tabPage1.Location = new System.Drawing.Point(4, 28);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(984, 576);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Function Aproximation";
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.num_res,
																				   this.label3,
																				   this.panel4,
																				   this.b_3d_drawnn,
																				   this.label2,
																				   this.b_3d_clearnn,
																				   this.b_3d_learn,
																				   this.panel3,
																				   this.b_3d_cpts});
			this.tabPage2.Location = new System.Drawing.Point(4, 28);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(984, 576);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Repartition Aproximation";
			// 
			// num_res
			// 
			this.num_res.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.num_res.DecimalPlaces = 3;
			this.num_res.Increment = new System.Decimal(new int[] {
																	  5,
																	  0,
																	  0,
																	  196608});
			this.num_res.Location = new System.Drawing.Point(192, 528);
			this.num_res.Maximum = new System.Decimal(new int[] {
																	5,
																	0,
																	0,
																	65536});
			this.num_res.Minimum = new System.Decimal(new int[] {
																	5,
																	0,
																	0,
																	196608});
			this.num_res.Name = "num_res";
			this.num_res.Size = new System.Drawing.Size(88, 23);
			this.num_res.TabIndex = 17;
			this.num_res.Value = new System.Decimal(new int[] {
																  25,
																  0,
																  0,
																  196608});
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(104, 530);
			this.label3.Name = "label3";
			this.label3.TabIndex = 16;
			this.label3.Text = "Resolution :";
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.demo3D_draw1});
			this.panel4.Location = new System.Drawing.Point(16, 72);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(600, 400);
			this.panel4.TabIndex = 15;
			// 
			// demo3D_draw1
			// 
			this.demo3D_draw1.BackColor = System.Drawing.Color.White;
			this.demo3D_draw1.Name = "demo3D_draw1";
			this.demo3D_draw1.Resolution = 0.025F;
			this.demo3D_draw1.Size = new System.Drawing.Size(600, 400);
			this.demo3D_draw1.TabIndex = 14;
			// 
			// b_3d_drawnn
			// 
			this.b_3d_drawnn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.b_3d_drawnn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_3d_drawnn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_3d_drawnn.Location = new System.Drawing.Point(304, 528);
			this.b_3d_drawnn.Name = "b_3d_drawnn";
			this.b_3d_drawnn.Size = new System.Drawing.Size(240, 23);
			this.b_3d_drawnn.TabIndex = 11;
			this.b_3d_drawnn.Text = "Draw neural network output";
			this.b_3d_drawnn.Click += new System.EventHandler(this.b_3d_drawnn_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(608, 32);
			this.label2.TabIndex = 13;
			this.label2.Text = "Demo of repartition aproximation by a neural network";
			// 
			// b_3d_clearnn
			// 
			this.b_3d_clearnn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.b_3d_clearnn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_3d_clearnn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_3d_clearnn.Location = new System.Drawing.Point(336, 488);
			this.b_3d_clearnn.Name = "b_3d_clearnn";
			this.b_3d_clearnn.Size = new System.Drawing.Size(240, 23);
			this.b_3d_clearnn.TabIndex = 9;
			this.b_3d_clearnn.Text = "Clear neural network output";
			this.b_3d_clearnn.Click += new System.EventHandler(this.b_3d_clearnn_Click);
			// 
			// b_3d_learn
			// 
			this.b_3d_learn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.b_3d_learn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_3d_learn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_3d_learn.Location = new System.Drawing.Point(208, 488);
			this.b_3d_learn.Name = "b_3d_learn";
			this.b_3d_learn.Size = new System.Drawing.Size(104, 23);
			this.b_3d_learn.TabIndex = 10;
			this.b_3d_learn.Text = "Learn";
			this.b_3d_learn.Click += new System.EventHandler(this.b_3d_learn_Click);
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.neuralNetworkEditor2});
			this.panel3.Location = new System.Drawing.Point(632, 8);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(336, 552);
			this.panel3.TabIndex = 12;
			// 
			// neuralNetworkEditor2
			// 
			this.neuralNetworkEditor2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.neuralNetworkEditor2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.neuralNetworkEditor2.Name = "neuralNetworkEditor2";
			this.neuralNetworkEditor2.Neural_Network = null;
			this.neuralNetworkEditor2.Size = new System.Drawing.Size(336, 552);
			this.neuralNetworkEditor2.TabIndex = 5;
			// 
			// b_3d_cpts
			// 
			this.b_3d_cpts.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.b_3d_cpts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.b_3d_cpts.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.b_3d_cpts.Location = new System.Drawing.Point(56, 488);
			this.b_3d_cpts.Name = "b_3d_cpts";
			this.b_3d_cpts.Size = new System.Drawing.Size(136, 23);
			this.b_3d_cpts.TabIndex = 8;
			this.b_3d_cpts.Text = "Clear points";
			this.b_3d_cpts.Click += new System.EventHandler(this.b_3d_cpts_Click);
			// 
			// Demo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(984, 598);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Demo";
			this.Text = "Neural Network Demo";
			this.Load += new System.EventHandler(this.Demo_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.num_res)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Demo());
		}

		private void b_clear_pts_Click(object sender, System.EventArgs e)
		{
			demo2D_Draw1.clearPoints();
		}

		private void b_learn_Click(object sender, System.EventArgs e)
		{
			demo2D_Draw1.setNeuralNetwork(neuralNetworkEditor1.Neural_Network);
			demo2D_Draw1.learn();
			neuralNetworkEditor1.refreshAll();

		}

		private void b_clear_draw_Click(object sender, System.EventArgs e)
		{
			demo2D_Draw1.Refresh();
		}

		private void b_draw_out_Click(object sender, System.EventArgs e)
		{
			demo2D_Draw1.Refresh();
			demo2D_Draw1.setNeuralNetwork(neuralNetworkEditor1.Neural_Network);
			demo2D_Draw1.drawNNOut();
		}

		private void Demo_Load(object sender, System.EventArgs e)
		{
		
		}

		private void b_3d_drawnn_Click(object sender, System.EventArgs e)
		{
			demo3D_draw1.Refresh();
			demo3D_draw1.setNeuralNetwork(neuralNetworkEditor2.Neural_Network);
			demo3D_draw1.Resolution = (float)this.num_res.Value;
			demo3D_draw1.drawNNOut();
		}

		private void b_3d_cpts_Click(object sender, System.EventArgs e)
		{
			demo3D_draw1.clearPoints();
		}

		private void b_3d_clearnn_Click(object sender, System.EventArgs e)
		{
			demo3D_draw1.Refresh();
		}

		private void b_3d_learn_Click(object sender, System.EventArgs e)
		{
			demo3D_draw1.setNeuralNetwork(neuralNetworkEditor2.Neural_Network);
			demo3D_draw1.learn();
			neuralNetworkEditor2.refreshAll();
		}

		
	}
}
