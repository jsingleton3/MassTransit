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
namespace MassTransit.Context
{
    using System;
    using System.Threading.Tasks;
    using Pipeline;


    public class ConsumeMessageSchedulerContext :
        MessageSchedulerContext
    {
        readonly ConsumeContext _consumeContext;
        readonly Uri _schedulerAddress;
        readonly Lazy<Task<ISendEndpoint>> _schedulerEndpoint;

        public ConsumeMessageSchedulerContext(ConsumeContext consumeContext, Uri schedulerAddress)
        {
            if (consumeContext == null)
                throw new ArgumentNullException(nameof(consumeContext));
            if (schedulerAddress == null)
                throw new ArgumentNullException(nameof(schedulerAddress));

            _consumeContext = consumeContext;
            _schedulerAddress = schedulerAddress;

            _schedulerEndpoint = new Lazy<Task<ISendEndpoint>>(GetSchedulerEndpoint);
        }

        public async Task ScheduleSend<T>(T message, TimeSpan deliveryDelay, IPipe<SendContext> sendPipe)
            where T : class
        {
            ISendEndpoint endpoint = await _schedulerEndpoint.Value.ConfigureAwait(false);

            await endpoint.ScheduleSend(_consumeContext.ReceiveContext.InputAddress, DateTime.UtcNow + deliveryDelay, message, sendPipe).ConfigureAwait(false);
        }

        public async Task ScheduleSend<T>(T message, DateTime deliveryTime, IPipe<SendContext> sendPipe)
            where T : class
        {
            ISendEndpoint endpoint = await _schedulerEndpoint.Value.ConfigureAwait(false);

            await endpoint.ScheduleSend(_consumeContext.ReceiveContext.InputAddress, deliveryTime, message, sendPipe).ConfigureAwait(false);
        }

        Task<ISendEndpoint> GetSchedulerEndpoint()
        {
            return _consumeContext.GetSendEndpoint(_schedulerAddress);
        }
    }
}