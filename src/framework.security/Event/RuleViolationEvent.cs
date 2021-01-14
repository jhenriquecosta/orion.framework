using System.Collections.Generic;
using Zeus.FluentRuleEngine;

namespace Zeus.Domain.Event
{
    /// <summary>
    /// Domain Event for Rule Violations.
    /// </summary>
    public class RuleViolationEvent : IDomainEvent
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the rule violations.
        /// </summary>
        /// <value>The rule violations.</value>
        public IEnumerable<RuleViolation> RuleViolations { get; set; }

        #endregion
    }
}
