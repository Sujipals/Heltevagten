namespace Heltevagten.Models
{
    /// <summary>
    /// Represents an incident reported to Heltevagten.
    /// </summary>
    public class Incident
    {
        public string Description { get; private set; }

        public string Location { get; private set; }

        public Severity Severity { get; private set; }

        public bool IsResolved { get; private set; }

        public string AssignedHeroName { get; private set; }

        public Incident(
            string description,
            string location,
            Severity severity)
        {
            Description = description;
            Location = location;
            Severity = severity;
            IsResolved = false;
            AssignedHeroName = null;
        }

        /// <summary>
        /// Assigns a hero to the incident.
        /// </summary>
        public void AssignHero(string heroName)
        {
            AssignedHeroName = heroName;
        }

        /// <summary>
        /// Marks the incident as resolved.
        /// </summary>
        public void Resolve()
        {
            IsResolved = true;
        }

        public override string ToString()
        {
            return Description
                + " | Location: "
                + Location
                + " | Severity: "
                + Severity
                + " | Resolved: "
                + IsResolved;
        }
    }
}