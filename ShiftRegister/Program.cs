using System.Drawing;
using System.Windows.Forms;

namespace ShiftRegister {
	internal class Program {
		public static void Main(string[] args) {
			CipherForm start = new CipherForm();
			start.Text = "Циклічний регістр зсуву";
			start.BackColor = Color.LightGray;
			Application.Run(start);
		}
	}
}