namespace PlanningPermissionApp
{
    public enum Location
    {
        AdjacentToHighway, // S1.1
        AdjacentToListedBuilding, // S1.2
        NotApplicable // S1.3
    }

    public enum Height
    {
        UpToOneMeter, // S2.1
        AboveOneMeter, // S2.2
        UpToTwoMeters, // S2.3
        AboveTwoMeters // S2.4
    }

    public enum PlanningConstraint
    {
        ListedBuilding, // S3.1
        Article2_3Land, // S3.2
        Article2_4Land, // S3.3
        Article4Directive, // S3.4
        AONB, // S3.5
        WorksAffectingTPO, // S3.6
        NotApplicable // S3.7
    }

    public enum Other
    {
        PDRemovedPreviousPlanning, // S4.1
        NewBuildPropertyRestrictions, // S4.2
        NotApplicable // S4.3
    }

    public class RestrictionsForm
    {
        public Location Location { get; set; }
        public Height Height { get; set; }
        public PlanningConstraint PlanningConstraint { get; set; }
        public Other Other { get; set; }

        public bool CheckPlanningPermission()
        {
            // First determine if it falls under the 'Universal' category
            bool isUniversalCategory = PlanningConstraint != PlanningConstraint.NotApplicable ||
                                       Other != Other.NotApplicable;

            // If it falls under the 'Universal' category, planning permission is required
            if (isUniversalCategory)
            {
                return true;
            }

            // If not in Universal category, check 'Other' category conditions
            bool isOtherCategory = (Location == Location.AdjacentToHighway && Height == Height.UpToOneMeter) ||
                                   (Location != Location.AdjacentToHighway && Height == Height.UpToTwoMeters);

            // Specific rules for 'Other' category when both PlanningConstraint and Other are 'NotApplicable'
            if (isOtherCategory && PlanningConstraint == PlanningConstraint.NotApplicable && Other == Other.NotApplicable)
            {
                return false;
            }

            // Default case where no specific conditions are met
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RestrictionsForm[] tests = {
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.ListedBuilding, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.Article2_3Land, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.Article2_4Land, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.Article4Directive, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.AONB, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.WorksAffectingTPO, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.PDRemovedPreviousPlanning },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NewBuildPropertyRestrictions },
                new RestrictionsForm { Location = Location.AdjacentToListedBuilding, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.AdjacentToHighway, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.AdjacentToHighway, Height = Height.UpToOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.AdjacentToHighway, Height = Height.AboveOneMeter, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToTwoMeters, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.UpToTwoMeters, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable },
                new RestrictionsForm { Location = Location.NotApplicable, Height = Height.AboveTwoMeters, PlanningConstraint = PlanningConstraint.NotApplicable, Other = Other.NotApplicable }
            };

            string[] titles = { "2U1", "2U2", "2U3", "2U4", "2U5", "2U6", "2U7", "2U8", "2U9", "2A1", "2A2", "2A3", "2A4", "2A5", "2A6" };

            for (int i = 0; i < tests.Length; i++)
            {
                bool result = tests[i].CheckPlanningPermission();
                Console.WriteLine($"{titles[i]}: Planning Permission Needed = {result}");
            }
        }
    }
}
