using Cake.Common.Tests.Fixtures.Tools;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using Xunit;

namespace Cake.Common.Tests.Unit.Tools.ReportGenerator
{
    public sealed class ReportGeneratorRunnerTests
    {
        public sealed class TheRunMethod
        {
            [Fact]
            public void Should_Throw_If_Reports_Are_Null()
            {
                // Given
                var fixture = new ReportGeneratorRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.Run(null, (DirectoryPath)"test", new ReportGeneratorSettings()));

                // Then
                Assert.IsArgumentNullException(result, "reports");
            }

            [Fact]
            public void Should_Throw_If_Reports_Are_Empty()
            {
                // Given
                var fixture = new ReportGeneratorRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.Run(new FilePath[0], (DirectoryPath)"test", new ReportGeneratorSettings()));

                // Then
                Assert.IsArgumentException(result, "reports", "reports must not be empty");
            }

            [Fact]
            public void Should_Throw_If_TargetDir_Is_Null()
            {
                // Given
                var fixture = new ReportGeneratorRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.Run(new[] { FilePath.FromString("test.xml") }, null, new ReportGeneratorSettings()));

                // Then
                Assert.IsArgumentNullException(result, "targetDir");
            }
        }

        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            var runner = fixture.CreateRunner();

            // When
            var result = Record.Exception(() => runner.Run(new[] { FilePath.FromString("test.xml") }, (DirectoryPath)"test", null));

            // Then
            Assert.IsArgumentNullException(result, "settings");
        }

        [Fact]
        public void Should_Find_Report_Generator()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            var runner = fixture.CreateRunner();

            // When
            runner.Run(new[] { FilePath.FromString("test.xml") }, DirectoryPath.FromString("output"), new ReportGeneratorSettings());

            // Then
            fixture.ProcessRunner.Received(1).Start(
                Arg.Is<FilePath>(p => p.FullPath == "/Working/tools/ReportGenerator.exe"),
                Arg.Any<ProcessSettings>()
                );
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            fixture.ProcessRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>()).Returns((IProcess)null);
            var runner = fixture.CreateRunner();

            // When
            var result = Record.Exception(() => runner.Run(new[] { FilePath.FromString("test.xml") }, DirectoryPath.FromString("output"), new ReportGeneratorSettings()));

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("ReportGenerator: Process was not started.", result.Message);
        }

        [Fact]
        public void Should_Throw_If_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            fixture.Process.GetExitCode().Returns(1);
            var runner = fixture.CreateRunner();

            // When
            var result = Record.Exception(() => runner.Run(new[] { FilePath.FromString("test.xml") }, DirectoryPath.FromString("output"), new ReportGeneratorSettings()));

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("ReportGenerator: Process returned an error.", result.Message);
        }

        [Fact]
        public void Should_Set_Report_And_Target_Directory()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            var runner = fixture.CreateRunner();

            // When
            runner.Run(new[] { FilePath.FromString("report.xml") }, DirectoryPath.FromString("output"), new ReportGeneratorSettings());

            // Then
            fixture.ProcessRunner.Received(1).Start(
                Arg.Any<FilePath>(),
                Arg.Any<ProcessSettings>());
            Assert.Equal("\"-reports:/Working/report.xml\" \"-targetdir:/Working/output\"", fixture.ProcessArguments);
        }

        [Fact]
        public void Should_Set_Reports_And_Target_Directory()
        {
            // Given
            var fixture = new ReportGeneratorRunnerFixture();
            var runner = fixture.CreateRunner();

            // When
            runner.Run(new[] { FilePath.FromString("report1.xml"), FilePath.FromString("report2.xml") }, DirectoryPath.FromString("output"), new ReportGeneratorSettings());

            // Then
            fixture.ProcessRunner.Received(1).Start(
                Arg.Any<FilePath>(),
                Arg.Any<ProcessSettings>());
            Assert.Equal("\"-reports:/Working/report1.xml;/Working/report2.xml\" \"-targetdir:/Working/output\"", fixture.ProcessArguments);
        }
    }
}