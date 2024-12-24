using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Task22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            MyHashMap<int, int> map = new MyHashMap<int, int>();
            MyTreeMap<int, int> tree = new MyTreeMap<int, int>();
            double srMap;
            double srTree;
            Random r = new Random();
            Stopwatch sw = new Stopwatch();
            int n = 20;
            int degFrom = 2;
            int degTo = 3;

            for (int g = degFrom; g <= degTo; g++)
            {
                srMap = 0;
                srTree = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < Math.Pow(10, g); j++)
                    {
                        sw.Reset();
                        sw.Start();
                        map.Put(r.Next(), r.Next());
                        sw.Stop();
                        srMap += sw.ElapsedMilliseconds;
                        sw.Reset();
                        sw.Start();
                        tree.Put(r.Next(), r.Next());
                        sw.Stop();
                        srTree += sw.ElapsedMilliseconds;
                    }
                }
                srMap /= n;
                srTree /= n;
                list1.Add(Math.Ceiling(Math.Pow(10, g)), srMap);
                list2.Add(Math.Ceiling(Math.Pow(10, g)), srTree);
            }
            LineItem myCurve1 = pane.AddCurve("MyHahsMap.Put", list1, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("MyHashTree.Put", list2, Color.Red, SymbolType.None);

            for (int g = degFrom; g <= degTo; g++)
            {
                srMap = 0;
                srTree = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < Math.Pow(10, g); j++)
                    {
                        sw.Reset();
                        sw.Start();
                        map.Get(r.Next());
                        sw.Stop();
                        srMap += sw.ElapsedMilliseconds;
                        sw.Reset();
                        sw.Start();
                        tree.Get(r.Next());
                        sw.Stop();
                        srTree += sw.ElapsedMilliseconds;
                    }
                }
                srMap /= n;
                srTree /= n;
                list1.Add(Math.Ceiling(Math.Pow(10, g)), srMap);
                list2.Add(Math.Ceiling(Math.Pow(10, g)), srTree);
            }
            LineItem myCurve3 = pane.AddCurve("MyHahsMap.Get", list1, Color.Green, SymbolType.None);
            LineItem myCurve4 = pane.AddCurve("MyHashTree.Get", list2, Color.Pink, SymbolType.None);
            for (int g = degFrom; g <= degTo; g++)
            {
                srMap = 0;
                srTree = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < Math.Pow(10, g); j++)
                    {
                        sw.Reset();
                        sw.Start();
                        map.Remove(r.Next());
                        sw.Stop();
                        srMap += sw.ElapsedMilliseconds;
                        sw.Reset();
                        sw.Start();
                        tree.Remove(r.Next());
                        sw.Stop();
                        srTree += sw.ElapsedMilliseconds;
                    }
                }
                srMap /= n;
                srTree /= n;
                list1.Add(Math.Ceiling(Math.Pow(10, g)), srMap);
                list2.Add(Math.Ceiling(Math.Pow(10, g)), srTree);
            }
            LineItem myCurve5 = pane.AddCurve("MyHahsMap.Remove", list1, Color.Purple, SymbolType.None);
            LineItem myCurve6 = pane.AddCurve("MyHashTree.Remove", list2, Color.Yellow, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }
    }
}