#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Template;
using MiniSqlQuery.Plugins.TemplateViewer;
using NUnit.Framework;

using Rhino.Mocks;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.Templates
{
	[TestFixture]
	public class TemplateModel_tests
	{
		private TemplateModel _model;
		private IApplicationServices _services;
		private IDatabaseInspector _databaseInspector;

		[SetUp]
		public void TestSetUp()
		{
			_services = MockRepository.GenerateStub<IApplicationServices>();
			_databaseInspector = MockRepository.GenerateStub<IDatabaseInspector>();
			_services.Expect(x => x.Resolve<TemplateHost>()).Return(new TemplateHost(_services, _databaseInspector));
			_model = new TemplateModel(_services, new NVelocityWrapper());
		}

		[Test]
		public void ModelData_parameters_are_precessed()
		{
			var items = new Dictionary<string, object>();
			string processedtext = _model.ProcessTemplate("create new ${Host.Date(\"yyyy\")}", items).Text;
			Assert.That(processedtext, Is.EqualTo("create new " + DateTime.Now.Year));
		}

		[Test]
		public void If_a_file_extension_is_set_this_defaults_the_TemplateResult()
		{
			var result = _model.ProcessTemplateFile(@"Templates\foo.cs.mt", null);
			Assert.That(result.Extension, Is.EqualTo("cs"));
		}

		[Test]
		public void If_no_file_extension_is_set_this_defaults_the_TemplateResult_to_SQL()
		{
			var result = _model.ProcessTemplateFile(@"Templates\bar.mt", null);
			Assert.That(result.Extension, Is.EqualTo("sql"));
		}
	}
}