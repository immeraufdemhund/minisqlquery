using System.Drawing;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Controls
{
	public class Interaction
	{
		public static string InputBox(string prompt, string title = "", string defaultResponse = "", int xPosition = -1, int yPosition = -1)
		{
			using (var form = new InputBoxForm(prompt) { Text = title })
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					return form.UserInput;
				}
			}
			return "";
		}

		private class InputBoxForm : Form
		{
			public string UserInput { get; private set; }

			public InputBoxForm(string prompt)
			{
				InitializeComponents(prompt);
			}

			private void InitializeComponents(string promptText)
			{
				// Create a new instance of the form.
				var prompt = new Label();
				var textBox = new TextBox();
				var buttonPanel = new TableLayoutPanel();

				buttonPanel.SuspendLayout();
				SuspendLayout();

				prompt.Dock = DockStyle.Fill;
				prompt.BackColor = SystemColors.Control;
				prompt.Text = promptText;
				prompt.Font = new Font("Microsoft Sans Serif", 8.25F);

				buttonPanel.Dock = DockStyle.Right;
				buttonPanel.Width = 80;
				buttonPanel.ColumnCount = 1;
				buttonPanel.ColumnStyles.Add(new ColumnStyle());
				buttonPanel.RowCount = 3;
				buttonPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
				buttonPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
				buttonPanel.RowStyles.Add(new RowStyle());
				buttonPanel.Controls.Add(SetupButton(DialogResult.OK), 0, 0);
				buttonPanel.Controls.Add(SetupButton(DialogResult.Cancel), 0, 1);

				textBox.Dock = DockStyle.Bottom;
				textBox.Height = 20;
				textBox.Text = "";
				textBox.TextChanged += (s, e) => { UserInput = ((Control)s).Text; };

				ClientSize = new Size(398, 128);
				Controls.Add(buttonPanel);
				Controls.Add(textBox);
				Controls.Add(prompt);
				FormBorderStyle = FormBorderStyle.FixedDialog;
				MaximizeBox = false;
				MinimizeBox = false;
				Name = "InputBoxDialog";
				Padding = new Padding(8);
				StartPosition = FormStartPosition.CenterParent;

				buttonPanel.ResumeLayout(false);
				ResumeLayout(false);
			}

			private static Button SetupButton(DialogResult dialogResult)
			{
				return new Button
				{
					DialogResult = dialogResult,
					Dock = DockStyle.Fill,
					FlatStyle = FlatStyle.Popup,
					Text = string.Format("&{0}", dialogResult),
				};
			}
		}
	}
}