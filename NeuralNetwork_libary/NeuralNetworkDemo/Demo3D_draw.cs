using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace NeuralNetworkDemo
{
	/// <summary>
	/// Summary description for Demo3D_draw.
	/// </summary>
	public class Demo3D_draw : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		protected ArrayList pts = new ArrayList();
		protected NeuralNetwork.NeuralNetwork nn;

		public static float X_MIN = 0f;
		public static float X_MAX = 1f;
		public static float Y_MIN = 0f;
		public static float Y_MAX = 1f;
		public static float RES = 0.025f;



		public Demo3D_draw()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
		}

		public void setNeuralNetwork(NeuralNetwork.NeuralNetwork n) 
		{
			nn = n;
		}

		public float Resolution 
		{
			get { return RES; }
			set { RES = value; }
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
			// Demo3D_draw
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "Demo3D_draw";
			this.Size = new System.Drawing.Size(600, 400);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graph_MouseDown);

		}
		#endregion

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
			drawPoints();
		}

		public void drawPoints() 
		{
			Graphics g = this.CreateGraphics();
			SolidBrush b1 = new SolidBrush(Color.Red);
			SolidBrush b2 = new SolidBrush(Color.Blue);
			foreach(UnPoint po in pts)
				if (po.v>0.5)
					g.FillEllipse(b1,po.X-3,po.Y-3,6,6);
				else
					g.FillEllipse(b2,po.X-3,po.Y-3,6,6);
		}

		public void clearPoints() 
		{
			pts.Clear();
			this.Refresh();
		}

		private void graph_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			UnPoint p =new UnPoint(e.X, e.Y, Width, Height);
			if (e.Button == MouseButtons.Left) 
				p.v = 1f;
			else 
				p.v = 0f;
			pts.Add(p);
			Graphics g = this.CreateGraphics();
			SolidBrush b;
			if (p.v>0.5)
				b = new SolidBrush(Color.Red);
			else
				b = new SolidBrush(Color.Blue);
			g.FillEllipse(b,p.X-3,p.Y-3,6,6);
		}

		public void drawNNOut() 
		{
			Graphics g = this.CreateGraphics();
			SolidBrush b4 = new SolidBrush(Color.FromArgb(255,192,192));
			SolidBrush b3 = new SolidBrush(Color.FromArgb(234,192,213));
			SolidBrush b2 = new SolidBrush(Color.FromArgb(217,192,229));
			SolidBrush b1 = new SolidBrush(Color.FromArgb(192,192,255));
			float z;
			float[] X = new float[2];
			int tx =(int)(RES * ((float)(Width -1))/(X_MAX-X_MIN));
			int ty = (int)(RES * ((float)(Height -1))/(Y_MAX-Y_MIN));
			for (float x=X_MIN; x<X_MAX; x+=RES) 
			{
				for (float y=Y_MIN; y<Y_MAX; y+=RES) 
				{
					X[0] = x;
					X[1] = y;
					z = nn.Output(X)[0];
					if (z<0.25)
						g.FillRectangle(b1,cX(x)-tx/2,cY(y)-ty/2,tx,ty);
					else if (z>=0.25 && z<0.5)
						g.FillRectangle(b2,cX(x)-tx/2,cY(y)-ty/2,tx,ty);
					else if (z>=0.5 && z<0.75)
						g.FillRectangle(b3,cX(x)-tx/2,cY(y)-ty/2,tx,ty);
					else
						g.FillRectangle(b4,cX(x)-tx/2,cY(y)-ty/2,tx,ty);

				}
			}
			drawPoints();
		}

		public void learn() 
		{
			float[][] X = new float[pts.Count][];
			float[][] Z = new float[pts.Count][];

			for(int i=0; i<pts.Count; i++)
			{
				X[i] = new float[2];
				Z[i] = new float[1];
				X[i][0] = ((UnPoint)pts[i]).x;
				X[i][1] = ((UnPoint)pts[i]).y;
				Z[i][0] = ((UnPoint)pts[i]).v;
			}
			if(pts.Count >0)
				nn.LearningAlg.Learn(X,Z);
			drawNNOut();
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
