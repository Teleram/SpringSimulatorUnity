using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NeuralNetwork;

namespace NeuralNetworkDemo
{
	/// <summary>
	/// Summary description for Demo2D_Draw.
	/// </summary>
	public class Demo2D_Draw : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected NeuralNetwork.NeuralNetwork nn;
		protected ArrayList pts = new ArrayList();
		public static float X_MIN = -1f;
		public static float X_MAX = 1f;
		public static float Y_MIN = 0f;
		public static float Y_MAX = 1f;

		public Demo2D_Draw()
		{
			
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}
		public void setNeuralNetwork(NeuralNetwork.NeuralNetwork n) 
		{
			nn = n;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Demo2D_Draw
			// 
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Name = "Demo2D_Draw";
			this.Size = new System.Drawing.Size(600, 400);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graph_MouseDown);

		}
		#endregion

		public void clearPoints() 
		{
			pts.Clear();
			this.Refresh();
		}

		private void graph_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			UnPoint p =new UnPoint(e.X, e.Y, Width, Height);
			pts.Add(p);
			Graphics g = this.CreateGraphics();
			SolidBrush b = new SolidBrush(Color.Red);
			g.FillEllipse(b,p.X-3,p.Y-3,6,6);
		}

		public void learn() 
		{
			float[][] X = new float[pts.Count][];
			float[][] Y = new float[pts.Count][];
				
			for(int i=0; i<pts.Count; i++)
			{
				X[i] = new float[1];
				Y[i] = new float[pts.Count];
				X[i][0] = (((UnPoint)pts[i]).x-X_MIN)/(X_MAX-X_MIN);
				Y[i][0] = ((UnPoint)pts[i]).y;
			}
			if (pts.Count > 0)
				nn.LearningAlg.Learn(X,Y);
			drawNNOut();
		}

		public void drawNNOut() 
		{
			Graphics g = this.CreateGraphics();
			Pen p = new Pen(new SolidBrush(Color.Blue),1);
			float y;
			float[] X = new float[1];
			for (float x=X_MIN; x<X_MAX; x+=0.002f) 
			{
				X[0] = (x-X_MIN)/(X_MAX-X_MIN);
				y = nn.Output(X)[0];
				g.DrawLine(p,cX(x), cY(y),cX(x),cY(y)+1);
			}
		}

		protected int cX(float x) 
		{
			return (int)(((float)(Width-1))/(X_MAX-X_MIN)*(x-X_MIN));
		}
		protected int cY(float y) 
		{
			return (Height-1) - (int)(((float)(Height-1))/(Y_MAX-Y_MIN)*(y-Y_MIN));
		}

		private void graph_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = this.CreateGraphics();
			Pen p = new Pen(new SolidBrush(Color.Black),1);

			// Le repere :
			//g.DrawLine(p,cX(0),cY(Y_MIN),cX(0),cY(Y_MAX));
			//g.DrawLine(p,cX(X_MIN),cY(0),cX(X_MAX),cY(0));
			// Les points
			SolidBrush b = new SolidBrush(Color.Red);
			foreach(UnPoint po in pts)
				g.FillEllipse(b,po.X-3,po.Y-3,6,6);
		}

		public class UnPoint 
		{
			public int X, Y;
			public float x, y;
			public float v;

			public UnPoint(int XX, int YY, int w, int h) 
			{
				X = XX;
				Y = YY;
				x = (X_MAX-X_MIN)/((float)(w-1))*((float)X) + X_MIN;
				y = (Y_MAX-Y_MIN)/((float)(h-1))*((float)((h-1) - Y)) + Y_MIN;
			}
			
			public UnPoint(float xx, float yy, int w, int h) 
			{
				x = xx;
				y = yy;
				X = (int)(((float)(w-1))/(X_MAX-X_MIN)*(x-X_MIN));
				Y = (h-1) - (int)(((float)(h-1))/(Y_MAX-Y_MIN)*(y-Y_MIN));
			}

			public void Refresh(int w, int h) 
			{
				X = (int)(((float)(w-1))/(X_MAX-X_MIN)*(x-X_MIN));
				Y = (h-1) - (int)(((float)(h-1))/(Y_MAX-Y_MIN)*(y-Y_MIN));
			}


		}
	}
}
