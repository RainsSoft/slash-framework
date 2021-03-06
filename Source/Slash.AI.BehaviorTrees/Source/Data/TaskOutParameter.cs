// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskOutParameter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.AI.BehaviorTrees.Data
{
    using Slash.AI.BehaviorTrees.Interfaces;

    /// <summary>
    ///   Container for a task parameter that is written to the blackboard.
    /// </summary>
    /// <typeparam name="T">Type of parameter.</typeparam>
    public class TaskOutParameter<T>
    {
        #region Properties

        /// <summary>
        ///   Key of blackboard attribute to store task parameter value to.
        /// </summary>
        public object BlackboardAttribute { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Sets the task parameter value.
        /// </summary>
        /// <param name="agentData">Agent data to set task parameter to.</param>
        /// <param name="value">Value of task parameter.</param>
        public void SetValue(IAgentData agentData, T value)
        {
            agentData.Blackboard.SetValue(this.BlackboardAttribute, value);
        }

        #endregion
    }
}