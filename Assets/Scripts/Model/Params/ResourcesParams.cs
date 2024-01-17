public static class ResourcesParams
{
    public static class Character
    {
        public static class Gender
        {
            public const string Male = "Character/Gender/" + nameof(Male);
            public const string Female = "Character/Gender/" + nameof(Female);
        }

        public static class Specialization
        {
            public const string MaleKnight = "Character/Specialization/" + nameof(MaleKnight);
            public const string MaleBarbarian = "Character/Specialization/" + nameof(MaleBarbarian);

            public const string FemaleKnight = "Character/Specialization/" + nameof(FemaleKnight);
            public const string FemaleBarbarian = "Character/Specialization/" + nameof(FemaleBarbarian);
        }
    }
}
