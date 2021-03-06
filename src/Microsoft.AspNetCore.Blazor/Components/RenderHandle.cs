﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Blazor.Rendering;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;

namespace Microsoft.AspNetCore.Blazor.Components
{
    /// <summary>
    /// Allows a component to notify the renderer that it should be rendered.
    /// </summary>
    public readonly struct RenderHandle
    {
        private readonly Renderer _renderer;
        private readonly int _componentId;

        internal RenderHandle(Renderer renderer, int componentId)
        {
            _renderer = renderer ?? throw new System.ArgumentNullException(nameof(renderer));
            _componentId = componentId;
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="RenderHandle"/> has been
        /// initialised and is ready to use.
        /// </summary>
        public bool IsInitalised
            => _renderer != null;

        /// <summary>
        /// Notifies the renderer that the component should be rendered.
        /// </summary>
        /// <param name="renderFragment">The content that should be rendered.</param>
        public void Render(RenderFragment renderFragment)
        {
            if (_renderer == null)
            {
                throw new InvalidOperationException("The render handle is not yet assigned.");
            }

            _renderer.AddToRenderQueue(new RenderQueueEntry(_componentId, renderFragment));
        }
    }
}
