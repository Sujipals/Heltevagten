
namespace Heltevagten.Models
{
    /// <summary>
    /// Represents an emergency incident reported to Heltevagten.
    /// </summary>
    public class Incident
    {
        public string Description { get; private set; }

        public string Location { get; private set; }

        public Severity Severity { get; private set; }

        public IncidentStatus Status { get; private set; }

        public string AssignedHeroName { get; private set; }
        public HeroSpecialty RequiredSpecialty { get; private set; }

        public Incident(
            string description,
            string location,
            Severity severity,
            HeroSpecialty requiredSpecialty)
        {
            Description = description;
            Location = location;
            Severity = severity;
            RequiredSpecialty = requiredSpecialty;

            // Every new incident starts as Open.
            Status = IncidentStatus.Open;

            AssignedHeroName = null;
        }

        /// <summary>
        /// Assigns a hero and changes the incident to InProgress.
        /// </summary>
        public void AssignHero(string heroName)
        {
            AssignedHeroName = heroName;
            Status = IncidentStatus.InProgress;
        }

        /// <summary>
        /// Marks the incident as resolved.
        /// </summary>
        public void Resolve()
        {
            Status = IncidentStatus.Resolved;
        }

        public override string ToString()
        {
            return Description
                + " | Location: "
                + Location
                + " | Severity: "
                + Severity
                + " | Required: "
                + RequiredSpecialty
                + " | Status: "
                + Status
                + " | Hero: "
                + (AssignedHeroName ?? "None");
        }
    }
}

