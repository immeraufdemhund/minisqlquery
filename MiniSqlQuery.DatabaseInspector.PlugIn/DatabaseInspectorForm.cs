﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;
using System.Media;
using System.Diagnostics;

namespace MiniSqlQuery.DatabaseInspector.PlugIn
{
	public partial class DatabaseInspectorForm : DockContent, IDatabaseInspector
	{
		/// <summary>
		/// Just for referencing.
		/// </summary>
		private interface ISchemaClass { }

		/// <summary>
		/// Simple helper class to identify a node as a table and store the schema info.
		/// </summary>
		private class TableInfo : ISchemaClass
		{
			public TableInfo(string schema, string name)
			{
				Schema = schema;
				Name = name;
			}
			public string Schema;
			public string Name;
		}

		/// <summary>
		/// Simple helper class to identify a node as a column and store it's schema info.
		/// </summary>
		private class ColumnInfo : ISchemaClass
		{
			public ColumnInfo(DataRow info)
			{
				ColumnSchemaDataRow = info;
			}
			public DataRow ColumnSchemaDataRow;
		}

		private DataTable _metaData;
		bool _populated;
		private TreeNode _rightClickedNode;

		internal IApplicationServices Services;
		internal DatabaseMetaDataService _metaDataService;

		private static object RootTag = new object();

		public DatabaseInspectorForm(IApplicationServices services)
		{
			InitializeComponent();
			DatabaseTreeView.Nodes.Clear();
			TreeNode root = CreateRootNode();
			root.Nodes.Add("Loading problem - check connection details and reset...");
			DatabaseTreeView.Nodes.Add(root);

			Services = services;

			Services.Settings.DatabaseConnectionReset += new EventHandler(Settings_DatabaseConnectionReset);
		}

		public string RightClickedTableName
		{
			get
			{
				string result = null;

				if (_rightClickedNode != null)
				{
					result = _rightClickedNode.Text;
				}

				return result;
			}
		}

		public DataTable DbSchema
		{
			get
			{
				return _metaData;
			}
		}

		public ContextMenuStrip TableMenu
		{
			get
			{
				return TableNodeContextMenuStrip;
			}
		}

		void Settings_DatabaseConnectionReset(object sender, EventArgs e)
		{
			_metaDataService = null;
			ExecLoadDatabaseDetails();
		}


		private void DatabaseInspectorControl_Load(object sender, EventArgs e)
		{
		}

		public void LoadDatabaseDetails()
		{
			ExecLoadDatabaseDetails();
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadDatabaseDetails();
		}

		private bool ExecLoadDatabaseDetails()
		{
			// todo - push into command?
			bool populate = false;
			string connection = string.Empty;
			bool success = false;

			try
			{
				Services.HostWindow.SetPointerState(Cursors.WaitCursor);
				if (_metaDataService == null)
				{
					_metaDataService = new DatabaseMetaDataService(
						Services.Settings.ProviderFactory,
						Services.Settings.ConnectionDefinition.ConnectionString);
				}
				connection = _metaDataService.GetDescription();
				populate = true;
			}
			catch (Exception exp)
			{
				string msg = string.Format(
					"{0}\r\n\r\nCheck the connection and select 'Reset Database Connection'.",
					exp.Message);
				Services.HostWindow.DisplaySimpleMessageBox(Services.HostWindow.Instance, msg, "DB Connection Error");
				Services.HostWindow.SetStatus(this, exp.Message);
			}
			finally
			{
				Services.HostWindow.SetPointerState(Cursors.Default);
			}

			if (populate)
			{
				if (_metaData != null)
				{
					_metaData.Dispose();
				}

				try
				{
					Services.HostWindow.SetPointerState(Cursors.WaitCursor);
					_metaData = _metaDataService.GetSchema();
				}
				finally
				{
					Services.HostWindow.SetPointerState(Cursors.Default);
				}

//#if DEBUG
//                _metaData.WriteXml("DbMetaData.xml", XmlWriteMode.WriteSchema);
//#endif

				DatabaseTreeView.Nodes.Clear();
				TreeNode root = CreateRootNode();
				root.ToolTipText = connection;

				// create a list of unique the schema.table names
				List<string> tableNames = new List<string>();
				DataView tablesDv = new DataView(_metaData, null, "Schema, Table", DataViewRowState.CurrentRows);
				foreach (DataRowView row in tablesDv)
				{
					string schemaName = (string)row["Schema"];
					string tableName = (string)row["Table"];
					string dot = ".";
					if (string.IsNullOrEmpty(schemaName)) // allow no schema
					{
						dot = string.Empty;
					}
					string fullTableName = string.Concat(schemaName, dot, tableName);

					if (!tableNames.Contains(fullTableName))
					{
						tableNames.Add(fullTableName);
					}
				}

				// loop tables and add column info
				foreach (string tableSchemaAndName in tableNames)
				{
					TreeNode tableNode = new TreeNode(tableSchemaAndName);
					tableNode.Name = tableSchemaAndName;
					tableNode.ImageKey = "Table";
					tableNode.SelectedImageKey = "Table";
					tableNode.ContextMenuStrip = this.TableNodeContextMenuStrip;
					string[] schemaTablePair = tableSchemaAndName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
					string schemaName = string.Empty;
					string tableName = string.Empty;
					if (schemaTablePair.Length == 1)
					{
						tableName = schemaTablePair[0];
					}
					else if (schemaTablePair.Length == 2)
					{
						schemaName = schemaTablePair[0];
						tableName = schemaTablePair[1];
					}

					string filter = string.Format("Table = '{0}'", tableName);
					if (string.IsNullOrEmpty(schemaName) == false)
					{
						filter += string.Format(" AND Schema = '{0}'", schemaName);
					}
					DataView columnsDv = new DataView(_metaData, filter, null, DataViewRowState.CurrentRows);
					tableNode.Tag = new TableInfo(tableName, schemaName);

					foreach (DataRowView rowView in columnsDv)
					{
						//rowView.Row
						int len = (int)rowView["Length"];
						string extra = rowView["DataType"].ToString();
						if (len > 0)
						{
							extra = string.Format("{0} ({1})", rowView["DataType"], len);
						}
						bool nullable = (bool)rowView["IsNullable"];
						if (nullable)
						{
							extra += " NULL";
						}
						else
						{
							extra += " NOT NULL";
						}
						string columnName = string.Format("{0} [{1}]", rowView["Column"], extra);
						TreeNode columnNode = new TreeNode(columnName);
						columnNode.Name = columnName;
						columnNode.ImageKey = "Column";
						columnNode.SelectedImageKey = "Column";
						columnNode.ContextMenuStrip = this.ColumnNameContextMenuStrip;
						columnNode.Tag = new ColumnInfo(rowView.Row);
						tableNode.Nodes.Add(columnNode);
					}
					root.Nodes.Add(tableNode);
				}

				DatabaseTreeView.Nodes.Add(root);
				Services.HostWindow.SetStatus(this, string.Empty);
				success = true;
			}
			else
			{
				_populated = false;
				DatabaseTreeView.CollapseAll();
			}

			return success;
		}

		private TreeNode CreateRootNode()
		{
			TreeNode root = new TreeNode("Database");
			root.ImageKey = "Database";
			root.SelectedImageKey = "Database";
			root.ContextMenuStrip = this.InspectorContextMenuStrip;
			root.Tag = RootTag;
			return root;
		}


		private void SetText(string text)
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;

			if (editor != null)
			{
				editor.InsertText(text);
			}
			else
			{
				SystemSounds.Beep.Play();
			}
		}

		private void DatabaseTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (e.Button == MouseButtons.Left)
			{
				if (node != null && (node.Tag is ISchemaClass))
				{
					string name = node.Text;
					if (node.Tag is TableInfo)
					{
						name = ((TableInfo)node.Tag).Name;
					}
					else if (node.Tag is ColumnInfo)
					{
						name = ((ColumnInfo)node.Tag).ColumnSchemaDataRow["Column"].ToString();
					}
					SetText(name);
				}
			}
		}

		private void DatabaseTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if (node != null && node.Tag == RootTag && !_populated)
			{
				_populated = true;
				bool ok = ExecLoadDatabaseDetails();

				if (ok && DatabaseTreeView.Nodes.Count > 0)
				{
					DatabaseTreeView.Nodes[0].Expand();
				}
				else
				{
					e.Cancel = true;
				}
			}

		}

		private void DatabaseInspectorForm_Load(object sender, EventArgs e)
		{

		}

		private void DatabaseTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (e.Button == MouseButtons.Right)
			{
				if (node != null && node.Tag is TableInfo)
				{
					_rightClickedNode = node;
					//Debug.WriteLine(node.Text);
				}
				else
				{
					_rightClickedNode = null;
				}
			}

		}

	}
}
