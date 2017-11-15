using System.ComponentModel;

namespace LocalAngle.Attractions
{
    /// <summary>
    /// An indicator to identify which attraction category
    /// </summary>
    /// <remarks>
    /// L codes are local.angle own,
    /// T codes are based on Schedule 14 of http://www.legislation.gov.uk/uksi/2002/3113/pdfs/uksi_20023113_en.pdf
    /// and https://www.gov.uk/working-drawings-for-traffic-signs#non-prescribed-drawings
    /// </remarks>
    public enum BrownSignCategory
    {
        /// <summary>
        /// Attraction type not known
        /// </summary>
        [Description("Attraction")]
        None = 0,

        /// <summary>
        /// Air museum
        /// </summary>
        [Description("Air Museum")]
        T117,

        /// <summary>
        /// Antiques Centre
        /// </summary>
        [Description("Antiques Centre")]
        T169,

        /// <summary>
        /// Aquarium
        /// </summary>
        [Description("Aquarium")]
        T126,

        /// <summary>
        /// Agricultural Museum
        /// </summary>
        [Description("Agricultural Museum")]
        T110,

        /// <summary>
        /// Battlefield site
        /// </summary>
        [Description("Battlefield site")]
        T154,

        /// <summary>
        /// Beach
        /// </summary>
        [Description("Beach")]
        T118,

        /// <summary>
        /// Bird garden
        /// </summary>
        [Description("Bird Garden")]
        T113,

        /// <summary>
        /// Bird of Prey Centre
        /// </summary>
        [Description("Bird of Prey Centre")]
        T150,

        /// <summary>
        /// Boat hire
        /// </summary>
        [Description("Boat Hire")]
        T141,

        /// <summary>
        /// Brass rubbing centre
        /// </summary>
        [Description("Brass rubbing centre")]
        T155,

        /// <summary>
        /// Brewery
        /// </summary>
        [Description("Brewing")]
        T165,

        /// <summary>
        /// Bus museum
        /// </summary>
        [Description("Bus museum")]
        T162,

        /// <summary>
        /// Butterfly farm
        /// </summary>
        [Description("Butterfly Farm")]
        T122,

        /// <summary>
        /// CADW site
        /// </summary>
        [Description("CADW")]
        T403,

        /// <summary>
        /// Camp site
        /// </summary>
        [Description("Camp site")]
        T7,

        /// <summary>
        /// Canalside attraction
        /// </summary>
        [Description("Canal-side Attraction")]
        T123,

        /// <summary>
        /// Canoeing Centre
        /// </summary>
        [Description("Canoeing")]
        T139,

        /// <summary>
        /// Caravan site
        /// </summary>
        [Description("Caravan Site")]
        T6,

        /// <summary>
        /// Castle
        /// </summary>
        [Description("Castle")]
        T2,

        /// <summary>
        /// Cathedral
        /// </summary>
        [Description("Cathedral")]
        T106,

        /// <summary>
        /// Church
        /// </summary>
        [Description("Church")]
        T105,

        /// <summary>
        /// Cider farm
        /// </summary>
        [Description("Cider Farm")]
        T166,

        /// <summary>
        /// Cinema
        /// </summary>
        [Description("Cinema")]
        T163,

        /// <summary>
        /// Country park
        /// </summary>
        [Description("Country Park")]
        T112,

        /// <summary>
        /// Craft centre
        /// </summary>
        [Description("Craft Centre or Forge")]
        T130,

        /// <summary>
        /// Cricket ground
        /// </summary>
        [Description("Cricket Ground")]
        T137,

        /// <summary>
        /// Cycle hire
        /// </summary>
        [Description("Cycle Hire")]
        T142,

        /// <summary>
        /// Distillery
        /// </summary>
        [Description("Distillery")]
        L7,

        /// <summary>
        /// English Heritage property
        /// </summary>
        [Description("English Heritage")]
        T202,

        /// <summary>
        /// English tourist board recognised attraction
        /// </summary>
        [Description("English tourist board recognised attraction")]
        T201,

        /// <summary>
        /// Equestrian Centre
        /// </summary>
        [Description("Equestrian Centre")]
        T111,

        /// <summary>
        /// Farm park
        /// </summary>
        [Description("Farm park")]
        T119,

        /// <summary>
        /// Farm trail
        /// </summary>
        [Description("Farm Trail")]
        T132,

        /// <summary>
        /// Fishing
        /// </summary>
        [Description("Fishing")]
        T140,

        /// <summary>
        /// Flower garden
        /// </summary>
        [Description("Flower Garden")]
        T102,

        /// <summary>
        /// Football Ground
        /// </summary>
        [Description("Football Ground")]
        T138,

        /// <summary>
        /// Forestry Commission
        /// </summary>
        [Description("Forestry Commission")]
        T304,

        /// <summary>
        /// Art gallery
        /// </summary>
        [Description("Gallery")]
        L6,

        /// <summary>
        /// Golf Course
        /// </summary>
        [Description("Golf Course")]
        T134,

        /// <summary>
        /// Heavy Horse Centre
        /// </summary>
        [Description("Heavy Horse Centre")]
        T128,

        /// <summary>
        /// Heritage rail or Railway Museum
        /// </summary>
        [Description("Heritage rail or Railway Museum")]
        T103,

        /// <summary>
        /// Historic building
        /// </summary>
        [Description("Historic building")]
        T157,

        /// <summary>
        /// Historic Dockyard / Maritime Museum
        /// </summary>
        [Description("Historic Dockyard / Maritime Museum")]
        T116,

        /// <summary>
        /// Historic house
        /// </summary>
        [Description("Historic House")]
        T3,

        /// <summary>
        /// Historic Scotland property
        /// </summary>
        [Description("Historic Scotland")]
        T302,

        /// <summary>
        /// Horse Racing
        /// </summary>
        [Description("Horse Racing")]
        T135,

        /// <summary>
        /// Hotel
        /// </summary>
        [Description("Hotel or other overnight accommodation")]
        T12,

        /// <summary>
        /// Ice Skating
        /// </summary>
        [Description("Ice Skating")]
        T147,

        /// <summary>
        /// Industrial Heritage
        /// </summary>
        [Description("Industrial Heritage")]
        T124,

        /// <summary>
        /// Light refreshments
        /// </summary>
        [Description("Light refreshments")]
        T10,

        /// <summary>
        /// Lighthouse
        /// </summary>
        [Description("Lighthouse open to public")]
        T158,

        /// <summary>
        /// Military Museum
        /// </summary>
        [Description("Military Museum")]
        T168,

        /// <summary>
        /// Motor museum
        /// </summary>
        [Description("Motor museum")]
        T129,

        /// <summary>
        /// Motor sport
        /// </summary>
        [Description("Motor Sport")]
        T136,

        /// <summary>
        /// Museum
        /// </summary>
        [Description("Museum / Gallery")]
        T203,

        /// <summary>
        /// Welsh Museum
        /// </summary>
        [Description("Museum / Gallery (Wales)")]
        T402,

        /// <summary>
        /// National Nature Reserve
        /// </summary>
        [Description("National Nature Reserve (English Nature)")]
        T205,

        /// <summary>
        /// National Trust property
        /// </summary>
        [Description("National Trust")]
        T101,

        /// <summary>
        /// National Trust fro Scotland property
        /// </summary>
        [Description("National Trust for Scotland")]
        T303,

        /// <summary>
        /// Nature Reserve
        /// </summary>
        [Description("Nature Reserve")]
        T115,

        /// <summary>
        /// Outdoor Pursuits centre
        /// </summary>
        [Description("Outdoor pursuit")]
        T145,

        /// <summary>
        /// Picnic area
        /// </summary>
        [Description("Picnic Area")]
        T4,

        /// <summary>
        /// Pier
        /// </summary>
        [Description("Pier")]
        T159,

        /// <summary>
        /// Theme Park
        /// </summary>
        [Description("Pleasure/Theme Park")]
        T114,

        /// <summary>
        /// Pottery or Craft Centre
        /// </summary>
        [Description("Pottery or Craft Centre")]
        T120,

        /// <summary>
        /// Prehistoric Site
        /// </summary>
        [Description("Prehistoric Site")]
        T121,

        /// <summary>
        /// Rare breeds farm
        /// </summary>
        [Description("Rare breeds")]
        T152,

        /// <summary>
        /// Restaurant
        /// </summary>
        [Description("Restaurant")]
        T11,

        /// <summary>
        /// Roller Skating
        /// </summary>
        [Description("Roller Skating")]
        T146,

        /// <summary>
        /// Roman Remains
        /// </summary>
        [Description("Roman Remains")]
        T127,

        /// <summary>
        /// RSPB Reserve
        /// </summary>
        [Description("RSPB Reserve")]
        T151,

        /// <summary>
        /// Rugby Ground
        /// </summary>
        [Description("Rugby Ground")]
        T167,

        /// <summary>
        /// Safari Park
        /// </summary>
        [Description("Safari Park")]
        T153,

        /// <summary>
        /// Scottish tourist board recognised attraction
        /// </summary>
        [Description("Scottish tourist board recognised attraction")]
        T301,

        /// <summary>
        /// Ski slope
        /// </summary>
        [Description("Ski Slope")]
        T148,

        /// <summary>
        /// Spa/Spring/Fountain
        /// </summary>
        [Description("Spa/Spring/Fountain")]
        T131,

        /// <summary>
        /// Sports Centre
        /// </summary>
        [Description("Sports Centre")]
        T204,

        /// <summary>
        /// Swimming Pool
        /// </summary>
        [Description("Swimming Pool")]
        T160,

        /// <summary>
        /// Ten Pin Bowling
        /// </summary>
        [Description("Ten Pin Bowling")]
        T149,

        /// <summary>
        /// Theatre / Concert hall
        /// </summary>
        [Description("Theatre / Concert hall")]
        T164,

        /// <summary>
        /// Tourist Information Centre
        /// </summary>
        [Description("Tourist Information Centre")]
        T1,

        /// <summary>
        /// Tower or folly
        /// </summary>
        [Description("Tower or folly")]
        T156,

        /// <summary>
        /// Tram museum
        /// </summary>
        [Description("Tram museum")]
        T161,

        /// <summary>
        /// Viewpoint
        /// </summary>
        [Description("Viewpoint")]
        T9,

        /// <summary>
        /// Vineyard
        /// </summary>
        [Description("Vineyard")]
        T133,

        /// <summary>
        /// Water sports
        /// </summary>
        [Description("Water sports")]
        T104,

        /// <summary>
        /// Watermill
        /// </summary>
        [Description("Watermill")]
        T125,

        /// <summary>
        /// Welsh tourist board recognised attraction
        /// </summary>
        [Description("Welsh tourist board recognised attraction")]
        T401,

        /// <summary>
        /// Wildlife Park
        /// </summary>
        [Description("Wildlife Park")]
        T107,

        /// <summary>
        /// Windmill
        /// </summary>
        [Description("Windmill")]
        T108,

        /// <summary>
        /// Woodland recreation
        /// </summary>
        [Description("Woodland recreation")]
        T8,

        /// <summary>
        /// Woodland Walk (coniferous)
        /// </summary>
        [Description("Woodland Walk (coniferous)")]
        T143,

        /// <summary>
        /// Woodland Walk (deciduous)
        /// </summary>
        [Description("Woodland Walk (deciduous)")]
        T144,

        /// <summary>
        /// Youth Hostel
        /// </summary>
        [Description("Youth Hostel")]
        T5,

        /// <summary>
        /// Zoo
        /// </summary>
        [Description("Zoo")]
        T109
    }
}
