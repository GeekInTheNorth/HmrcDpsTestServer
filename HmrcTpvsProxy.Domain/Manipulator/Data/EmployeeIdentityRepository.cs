using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Manipulator.Data
{
    public class EmployeeIdentityRepository : IEmployeeIdentityRepository
    {
        public IEnumerable<EmployeeIdentity> Get()
        {
            return new List<EmployeeIdentity>
            {
                new EmployeeIdentity(135, "JK258147D", "Fiona", "Cameron", "Miss"),
                new EmployeeIdentity(136, "ZB394740B", "Raymond", "Jackson", "Mr"),
                new EmployeeIdentity(137, "JS123459A", "Pay ID", "Woman", "Mrs"),
                new EmployeeIdentity(138, "JC998763A", "James", "Brown", "Mr"),
                new EmployeeIdentity(139, "ZR437494D", "Jung", "Chang", "Ms"),
                new EmployeeIdentity(140, "JB662203", "Tony", "Jones", "Mr"),
                new EmployeeIdentity(142, "NZ497307C", "Sean", "Martin", "Mr"),
                new EmployeeIdentity(143, "JH987643A", "NiChg", "Leaver", "Mr"),
                new EmployeeIdentity(145, "JC678437D", "Pamela", "Hamilton", "Miss"),
                new EmployeeIdentity(146, "JS123458A", "Pensions", "Before Tax", "Mrs"),
                new EmployeeIdentity(147, "NY199397D", "Dukes", "Martin", "Mr"),
                new EmployeeIdentity(148, "AB157834D", "Frank", "Herbert", "Mr"),
                new EmployeeIdentity(149, "WK044719B", "Michael", "Beeman", "Mr"),
                new EmployeeIdentity(150, "WK122111D", "Darryl", "Geoghan", "Mr"),
                new EmployeeIdentity(151, "AB123987C", "Geoffrey", "Browns", "Mr"),
                new EmployeeIdentity(152, "ZT114940B", "Adam", "Gray", "Mr"),
                new EmployeeIdentity(153, "ZW449334A", "Alex", "Caulfield", "Mr"),
                new EmployeeIdentity(155, "NP217237", "George", "Benson", "Mr"),
                new EmployeeIdentity(156, "SW755701A", "Bridget", "Jones", "Miss"),
                new EmployeeIdentity(157, "JN180014A", "Victoria", "Bennett", "Mrs"),
                new EmployeeIdentity(158, "JK756428A", "Avril", "Lawlor", "Ms"),
                new EmployeeIdentity(159, "JK436587D", "Richard", "King", "Mr"),
                new EmployeeIdentity(160, "JS123452A", "SMP", "Cat D", "Mrs"),
                new EmployeeIdentity(161, "YK123456A", "Thomas", "Jackson", "Mr"),
                new EmployeeIdentity(162, "OS845118P", "Paul", "Long", "Mr"),
                new EmployeeIdentity(163, "SW756754", "Less", "Weeks", "Mr"),
                new EmployeeIdentity(164, "HY526329M", "Joe", "Jackson", "Mr"),
                new EmployeeIdentity(165, "AA444444D", "Sarah", "Bradshaw", "Miss"),
                new EmployeeIdentity(166, "JL123456A", "Anthony", "Morgan", "Mr"),
                new EmployeeIdentity(167, "JX817272A", "Bernard", "Brittle", "Dr"),
                new EmployeeIdentity(168, "NW440030B", "Simon", "O'Donnell", "Mr"),
                new EmployeeIdentity(169, "JZ844018C", "Tracy", "Mantle", "Miss"),
                new EmployeeIdentity(170, "JS123457A", "Director", "Directorson", "Mr"),
                new EmployeeIdentity(172, "JP984454D", "Richard", "Brown", "Mr"),
                new EmployeeIdentity(173, "OS358512B", "Aaron", "Tingle", "Mr"),
                new EmployeeIdentity(174, "ZT344793C", "Rick", "Symons", "Mr"),
                new EmployeeIdentity(175, "WP904239A", "Sophie", "Lewis", "Miss"),
                new EmployeeIdentity(176, "RE675674D", "Gemma", "Williamson", "Miss"),
                new EmployeeIdentity(177, "LL918712C", "Hakam", "Khan", "Mr"),
                new EmployeeIdentity(178, "ZM370199A", "Richard", "Wyatt", "Mr"),
                new EmployeeIdentity(180, "WE785789D", "Cara", "Houghton", "Miss"),
                new EmployeeIdentity(181, "BM802103D", "Lloyd", "Jones", "Mr"),
                new EmployeeIdentity(182, "KS694280B", "Rachael", "Washington", "Mrs"),
                new EmployeeIdentity(184, "AB123987C", "John", "Moore", "Mr"),
                new EmployeeIdentity(185, "OR287110D", "Erica", "Wing", "Miss"),
                new EmployeeIdentity(186, "AB123987C", "Robert", "Tanner", "Mr"),
                new EmployeeIdentity(187, "JA213251D", "Prasant", "Patel", "Mr"),
                new EmployeeIdentity(188, "AB123987C", "Darryl", "Riddell", "Mr"),
                new EmployeeIdentity(189, "AB123987C", "Richard", "Kimble", "Dr"),
                new EmployeeIdentity(190, "JZ638263C", "Neil", "Hurst", "Mr"),
                new EmployeeIdentity(192, "AB123987C", "Jerry", "Springer", "Mr"),
                new EmployeeIdentity(193, "WE710933A", "Allan", "O'Neil", "Mr"),
                new EmployeeIdentity(194, "AB123987C", "Joseph", "Wilson", "Mr"),
                new EmployeeIdentity(195, "LZ863747A", "Roger", "Healy", "Mr"),
                new EmployeeIdentity(196, "AB123987C", "Kelvin", "Richardson", "Mr"),
                new EmployeeIdentity(197, "JZ140686B", "Leonardo", "Tonks", "Mr"),
                new EmployeeIdentity(198, "YY567890A", "Alex", "Tait", "Mr"),
                new EmployeeIdentity(199, "WP414255F", "Keith", "Jones", "Mr"),
                new EmployeeIdentity(200, "YY118817C", "Donald", "Wicks", "Mr"),
                new EmployeeIdentity(201, "AH469040B", "Matthew", "Right", "Mr"),
                new EmployeeIdentity(202, "AA478585A", "Kenneth", "Rubble", "Mr"),
                new EmployeeIdentity(203, "NR743470C", "Graeme", "Ward", "Mr"),
                new EmployeeIdentity(204, "NB404374A", "James", "Hepburn", "Mr"),
                new EmployeeIdentity(205, "ZX904709C", "Leon", "Hepper", "Mr"),
                new EmployeeIdentity(206, "AB123987C", "Susan", "Spencer", "Mrs"),
                new EmployeeIdentity(207, "AB123987C", "Sylvia", "Smith", "Mrs"),
                new EmployeeIdentity(208, "NA934333B", "Steven", "Woodward", "Mr"),
                new EmployeeIdentity(209, "AB123987C", "Trevor", "Rashid", "Mr"),
                new EmployeeIdentity(210, "JY794940A", "Jeff", "Murray", "Mr"),
                new EmployeeIdentity(211, "WL801152A", "Raymond", "Sayer", "Mr"),
                new EmployeeIdentity(212, "AB123987C", "John", "Smith", "Mr"),
                new EmployeeIdentity(213, "YR386795C", "Robert", "Parker", "Mr"),
                new EmployeeIdentity(215, "JX109264A", "Dani", "Devon", "Mr"),
                new EmployeeIdentity(216, "ZR910771A", "Lynn", "Marshall", "Mrs"),
                new EmployeeIdentity(217, "JX164892A", "Frank", "Fernando", "Mr"),
                new EmployeeIdentity(218, "KE995536", "Ellie", "Goodrich", "Ms"),
                new EmployeeIdentity(219, "SG314720A", "Montgomery", "Burns", "Mr"),
                new EmployeeIdentity(220, "NE994944C", "David", "Fountain", "Mr"),
                new EmployeeIdentity(221, "WM444444B", "Helen", "Harvey", "Mrs"),
                new EmployeeIdentity(222, "JZ333333D", "Charlie", "Conroy", "Miss"),
                new EmployeeIdentity(223, "AA555555D", "Ashley", "Mackenzie", "Mrs"),
                new EmployeeIdentity(224, "JT123456A", "Neil", "Tierney", "Mr"),
                new EmployeeIdentity(225, "JB163740C", "Yvonne", "Baker", "Mrs"),
                new EmployeeIdentity(226, "JP234567D", "Daisy", "Johnson", "Miss"),
                new EmployeeIdentity(227, "JK123456A", "Jack", "Jacobs", "Dr"),
                new EmployeeIdentity(228, "JH123789A", "Craig", "Fuller", "Dr"),
                new EmployeeIdentity(229, "CK333333A", "Jonathan", "Moore", "Mr"),
                new EmployeeIdentity(231, "PT121819", "Joanna", "Dean", "Mrs"),
                new EmployeeIdentity(236, "JN123456D", "Joey", "Tribbiani", "Mr"),
                new EmployeeIdentity(1236, "JN123456D", "James", "Boothman", "Mr"),
                new EmployeeIdentity(1237, "JX283761A", "Aman", "Paul", "Dr"),
                new EmployeeIdentity(1238, "JX434332A", "Aman", "Payroll", "Dr"),
                new EmployeeIdentity(1239, "JS123453A", "New ", "Starter", "Mr"),
                new EmployeeIdentity(1240, "JK123456A", "New", "Pensioner", "Mr"),
                new EmployeeIdentity(1241, "JS123454A", "New", "Starter Eea", "Mr"),
                new EmployeeIdentity(1242, "JK123456A", "Mark", "Rodley", "Mr"),
                new EmployeeIdentity(1243, "LS387720D", "Jack", "Land", "Mr"),
                new EmployeeIdentity(1244, "NR334943B", "Dawn", "Beeby", "Ms")
            };
        }
    }
}