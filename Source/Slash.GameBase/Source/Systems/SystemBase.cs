﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.GameBase.Systems
{
    using Slash.Collections.AttributeTables;

    /// <summary>
    ///   Base system class.
    /// </summary>
    public class SystemBase : ISystem
    {
        #region Public Properties

        /// <summary>
        ///   Game this system belongs to.
        /// </summary>
        public Game Game { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Initializes this system with the data stored in the specified
        ///   attribute table.
        /// </summary>
        /// <param name="configuration">System configuration data.</param>
        public virtual void Init(IAttributeTable configuration)
        {
        }

        /// <summary>
        ///   Ticks this system.
        /// </summary>
        /// <param name="dt">
        ///   Time passed since the last tick, in seconds.
        /// </param>
        public virtual void UpdateSystem(float dt)
        {
        }

        #endregion
    }
}