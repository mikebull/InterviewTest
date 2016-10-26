﻿using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace FundsLibrary.InterviewTest.Service
{
    public class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorHttpControllerActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(
                new _ControllerReleaser(_container, controller));

            return controller;
        }

        private class _ControllerReleaser : IDisposable
        {
            private readonly IWindsorContainer _container;
            private readonly IHttpController _controller;

            public _ControllerReleaser(IWindsorContainer container, IHttpController controller)
            {
                _container = container;
                _controller = controller;
            }

            public void Dispose()
            {
                _container.Release(_controller);
            }
        }
    }
}