using System.Collections.Generic;
using Cake.Common.Tests.Fixtures.Tools;
using Cake.Common.Tools.DupFinder;
using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using Xunit;

namespace Cake.Common.Tests.Unit.Tools.DupFinder
{
    public sealed class DupFinderRunnerTests
    {
        public sealed class TheRunMethodWithFiles
        {
            [Fact]
            public void Should_Throw_If_Projects_Are_Null()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.Run((IEnumerable<FilePath>)null, new DupFinderSettings()));

                // Then
                Assert.IsArgumentNullException(result, "projects");
            }

            [Fact]
            public void Should_Find_Inspect_Code_Runner()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                runner.Run(new[] { FilePath.FromString("./Test.sln") }, new DupFinderSettings());

                // Then
                fixture.ProcessRunner.Received(1).Start(
                    Arg.Is<FilePath>(p => p.FullPath == "/Working/tools/dupfinder.exe"),
                    Arg.Any<ProcessSettings>());
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                fixture.ProcessRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>()).Returns((IProcess)null);
                var runner = fixture.CreateRunner();

                // When
                var result =
                    Record.Exception(
                        () => runner.Run(new[] { FilePath.FromString("./Test.sln") }, new DupFinderSettings()));

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("DupFinder: Process was not started.", result.Message);
            }

            [Fact]
            public void Should_Throw_If_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                fixture.Process.GetExitCode().Returns(1);
                var runner = fixture.CreateRunner();

                // When
                var result =
                    Record.Exception(
                        () => runner.Run(new[] { FilePath.FromString("./Test.sln") }, new DupFinderSettings()));

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("DupFinder: Process returned an error.", result.Message);
            }

            [Fact]
            public void Should_Use_Provided_Files_In_Process_Arguments()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                runner.Run(new[] { FilePath.FromString("./Test.sln"), FilePath.FromString("./Test.csproj") }, new DupFinderSettings());

                // Then
                fixture.ProcessRunner.Received(1).Start(
                    Arg.Any<FilePath>(),
                    Arg.Any<ProcessSettings>());
                Assert.Equal("\"/Working/Test.sln\" \"/Working/Test.csproj\"", fixture.ProcessArguments);
            }

            [Fact]
            public void Should_Set_Output_File()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // Then
                runner.Run(new[] { FilePath.FromString("./Test.sln") }, new DupFinderSettings
                {
                    OutputFile = FilePath.FromString("build/dupfinder.xml")
                });

                // Then
                fixture.ProcessRunner.Received(1).Start(
                    Arg.Any<FilePath>(),
                    Arg.Any<ProcessSettings>());
                Assert.Equal("\"/output=/Working/build/dupfinder.xml\" \"/Working/Test.sln\"", fixture.ProcessArguments);
            }
        }

        public sealed class TheRunMethodWithPattern
        {
            [Fact]
            public void Should_Throw_If_Pattern_Is_Null()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.Run((string)null, new DupFinderSettings()));

                // Then
                Assert.IsArgumentNullException(result, "pattern");
            }
        }

        public sealed class TheRunFromConfigMethod
        {
            [Fact]
            public void Should_Throw_If_Config_File_Is_Null()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                var result = Record.Exception(() => runner.RunFromConfig((FilePath)null));

                // Then
                Assert.IsArgumentNullException(result, "configFile");
            }

            [Fact]
            public void Should_Use_Provided_Config_File()
            {
                // Given
                var fixture = new DupFinderRunnerFixture();
                var runner = fixture.CreateRunner();

                // When
                runner.RunFromConfig(FilePath.FromString("config.xml"));

                // Then
                fixture.ProcessRunner.Received(1).Start(
                    Arg.Any<FilePath>(),
                    Arg.Any<ProcessSettings>());
                Assert.Equal("\"/config=/Working/config.xml\"", fixture.ProcessArguments);
            }
        }
    }
}