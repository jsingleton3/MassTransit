﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using PipeConfigurators;


    public static class MessageSchedulerExtensions
    {
        /// <summary>
        /// Specify an endpoint to use for message scheduling
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="schedulerAddress"></param>
        public static void UseMessageScheduler(this IPipeConfigurator<ConsumeContext> configurator, Uri schedulerAddress)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var pipeBuilderConfigurator = new MessageSchedulerPipeSpecification(schedulerAddress);

            configurator.AddPipeSpecification(pipeBuilderConfigurator);
        }
    }
}