using MiniSqlQuery.Core;

namespace MiniSqlQuery.Plugins.ConnectionStringsManager
{
	public class DbConnectionsFormDialog
	{
		public static void Show(IHostWindow window)
		{
			using (var form = ApplicationServices.Instance.Resolve<DbConnectionsForm>())
			{
				form.ShowDialog(window.Instance);
			}
		}
	}
}
