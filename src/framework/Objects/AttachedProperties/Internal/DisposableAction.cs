﻿#region License
// Copyright (c) Niklas Wendel 2016-2017
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using System;

namespace Orion.Framework.Objects.AttachedProperties.Internal
{

    /// <summary>
    /// 
    /// </summary>
    internal class DisposableAction : IDisposable
    {

        #region Fields

        private readonly Action _disposableAction;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="disposableAction"></param>
        internal DisposableAction(Action action, Action disposableAction)
        {
            action();
            _disposableAction = disposableAction;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _disposableAction();
        }

        #endregion

    }

}
