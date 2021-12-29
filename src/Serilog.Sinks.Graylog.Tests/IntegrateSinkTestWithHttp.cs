﻿using System;
using System.Linq;
using AutoFixture;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Exceptions;
using Xunit;
using Serilog.Sinks.Graylog.Core.Helpers;
using Serilog.Sinks.Graylog.Core.Transport;
using Serilog.Sinks.Graylog.Tests.ComplexIntegrationTest;

namespace Serilog.Sinks.Graylog.Tests
{
    [Trait("Category", "Integration")]
    public class IntegrateSinkTestWithHttp
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void TestComplex()
        {
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                ShortMessageMaxLength = 50,
                MinimumLogEventLevel = LogEventLevel.Information,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            var test = new TestClass
            {
                Id = 1,
                Bar = new Bar
                {
                    Id = 2,
                    Prop = "123"
                },
                TestPropertyOne = "1",
                TestPropertyThree = "3",
                TestPropertyTwo = "2"
            };

            logger.Information("SomeComplexTestEntry {@test}", test);
        }

        [Fact]
        public void WhenHostIsWrong_ThenLoggerCreationShouldNotBeFail()
        {
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                ShortMessageMaxLength = 50,
                MinimumLogEventLevel = LogEventLevel.Information,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "abracadabra",
                Port = 12201,
                SerializerSettings = new JsonSerializerSettings()
            });

            var logger = loggerConfig.CreateLogger();

            var test = new TestClass
            {
                Id = 1,
                Bar = new Bar
                {
                    Id = 2,
                    Prop = "123"
                },
                TestPropertyOne = "1",
                TestPropertyThree = "3",
                TestPropertyTwo = "2",
                EnumVal = TestEnumOne.Three,
                SomeTestDateTime = DateTime.Now
                    
                
            };

            logger.Information("SomeComplexTestEntry {@test}", test);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithLevel()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Error,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }


        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithOneProfile()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }

        [Fact]
        [Trait("Ignore", "Integration")]
        public void Log10Profiles()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profiles = fixture.CreateMany<Profile>(10).ToList();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            profiles.AsParallel().ForAll(profile =>
            {
                logger.Information("TestSend {@BattleProfile}", profile);
            });
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithUsernamePassword()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201,
                UsernameInHttp ="username",
                PasswordInHttp ="password"
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithHostnameOrAddressEndsWithoutPath()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithHostnameOrAddressEndsWithSlash()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int/",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LogInformationWithHostnameOrAddressEndsWithPath()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Clear();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var profile = fixture.Create<Profile>();

            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int/testgelf",
                Port = 12201
            });

            var logger = loggerConfig.CreateLogger();

            logger.Information("battle profile:  {@BattleProfile}", profile);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestException()
        {
            var loggerConfig = new LoggerConfiguration();

            loggerConfig
                .Enrich.WithExceptionDetails()
                .WriteTo.Graylog(new GraylogSinkOptions
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                MessageGeneratorType = MessageIdGeneratorType.Timestamp,
                TransportType = TransportType.Http,
                Facility = "VolkovTestFacility",
                HostnameOrAddress = "http://logs.aeroclub.int",
                Port = 12201
            });

            var test = new TestClass
            {
                Id = 1,
                SomeTestDateTime = DateTime.UtcNow,
                Bar = new Bar
                {
                    Id = 2
                },
                TestPropertyOne = "1",
                TestPropertyThree = "3",
                TestPropertyTwo = "2"
            };


            var logger = loggerConfig.CreateLogger();

            try
            {
                try
                {
                    throw new InvalidOperationException("Level One exception");
                }
                catch (Exception exc)
                {
                    throw new NotImplementedException("Nested Exception", exc);
                }
            }
            catch (Exception exc)
            {
                logger.Error(exc, "test exception with object {@test}", test);
            }
        }

    }
}