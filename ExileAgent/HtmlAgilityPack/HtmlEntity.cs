using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlEntity
	{
		public static bool UseWebUtility { get; set; }

		public static Dictionary<int, string> EntityName
		{
			get
			{
				return HtmlEntity._entityName;
			}
		}

		public static Dictionary<string, int> EntityValue
		{
			get
			{
				return HtmlEntity._entityValue;
			}
		}

		static HtmlEntity()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlEntity));
			HtmlEntity._entityName = new Dictionary<int, string>();
			HtmlEntity._entityValue = new Dictionary<string, int>();
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245361), 34);
			HtmlEntity._entityName.Add(34, HtmlEntity.getString_0(107245361));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245352), 38);
			HtmlEntity._entityName.Add(38, HtmlEntity.getString_0(107245352));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244803), 39);
			HtmlEntity._entityName.Add(39, HtmlEntity.getString_0(107244803));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244794), 60);
			HtmlEntity._entityName.Add(60, HtmlEntity.getString_0(107244794));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244789), 62);
			HtmlEntity._entityName.Add(62, HtmlEntity.getString_0(107244789));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244816), 160);
			HtmlEntity._entityName.Add(160, HtmlEntity.getString_0(107244816));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244807), 161);
			HtmlEntity._entityName.Add(161, HtmlEntity.getString_0(107244807));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244766), 162);
			HtmlEntity._entityName.Add(162, HtmlEntity.getString_0(107244766));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244757), 163);
			HtmlEntity._entityName.Add(163, HtmlEntity.getString_0(107244757));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244780), 164);
			HtmlEntity._entityName.Add(164, HtmlEntity.getString_0(107244780));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244739), 165);
			HtmlEntity._entityName.Add(165, HtmlEntity.getString_0(107244739));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244734), 166);
			HtmlEntity._entityName.Add(166, HtmlEntity.getString_0(107244734));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244725), 167);
			HtmlEntity._entityName.Add(167, HtmlEntity.getString_0(107244725));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244748), 168);
			HtmlEntity._entityName.Add(168, HtmlEntity.getString_0(107244748));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244743), 169);
			HtmlEntity._entityName.Add(169, HtmlEntity.getString_0(107244743));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244702), 170);
			HtmlEntity._entityName.Add(170, HtmlEntity.getString_0(107244702));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244693), 171);
			HtmlEntity._entityName.Add(171, HtmlEntity.getString_0(107244693));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244716), 172);
			HtmlEntity._entityName.Add(172, HtmlEntity.getString_0(107244716));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244711), 173);
			HtmlEntity._entityName.Add(173, HtmlEntity.getString_0(107244711));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244674), 174);
			HtmlEntity._entityName.Add(174, HtmlEntity.getString_0(107244674));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244669), 175);
			HtmlEntity._entityName.Add(175, HtmlEntity.getString_0(107244669));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244660), 176);
			HtmlEntity._entityName.Add(176, HtmlEntity.getString_0(107244660));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244687), 177);
			HtmlEntity._entityName.Add(177, HtmlEntity.getString_0(107244687));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244678), 178);
			HtmlEntity._entityName.Add(178, HtmlEntity.getString_0(107244678));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244637), 179);
			HtmlEntity._entityName.Add(179, HtmlEntity.getString_0(107244637));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244628), 180);
			HtmlEntity._entityName.Add(180, HtmlEntity.getString_0(107244628));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244651), 181);
			HtmlEntity._entityName.Add(181, HtmlEntity.getString_0(107244651));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244610), 182);
			HtmlEntity._entityName.Add(182, HtmlEntity.getString_0(107244610));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244601), 183);
			HtmlEntity._entityName.Add(183, HtmlEntity.getString_0(107244601));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244624), 184);
			HtmlEntity._entityName.Add(184, HtmlEntity.getString_0(107244624));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244615), 185);
			HtmlEntity._entityName.Add(185, HtmlEntity.getString_0(107244615));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244574), 186);
			HtmlEntity._entityName.Add(186, HtmlEntity.getString_0(107244574));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244565), 187);
			HtmlEntity._entityName.Add(187, HtmlEntity.getString_0(107244565));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244588), 188);
			HtmlEntity._entityName.Add(188, HtmlEntity.getString_0(107244588));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245059), 189);
			HtmlEntity._entityName.Add(189, HtmlEntity.getString_0(107245059));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245050), 190);
			HtmlEntity._entityName.Add(190, HtmlEntity.getString_0(107245050));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245073), 191);
			HtmlEntity._entityName.Add(191, HtmlEntity.getString_0(107245073));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245064), 192);
			HtmlEntity._entityName.Add(192, HtmlEntity.getString_0(107245064));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245023), 193);
			HtmlEntity._entityName.Add(193, HtmlEntity.getString_0(107245023));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245014), 194);
			HtmlEntity._entityName.Add(194, HtmlEntity.getString_0(107245014));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245037), 195);
			HtmlEntity._entityName.Add(195, HtmlEntity.getString_0(107245037));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245028), 196);
			HtmlEntity._entityName.Add(196, HtmlEntity.getString_0(107245028));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244987), 197);
			HtmlEntity._entityName.Add(197, HtmlEntity.getString_0(107244987));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245010), 198);
			HtmlEntity._entityName.Add(198, HtmlEntity.getString_0(107245010));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107245001), 199);
			HtmlEntity._entityName.Add(199, HtmlEntity.getString_0(107245001));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244960), 200);
			HtmlEntity._entityName.Add(200, HtmlEntity.getString_0(107244960));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244951), 201);
			HtmlEntity._entityName.Add(201, HtmlEntity.getString_0(107244951));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244974), 202);
			HtmlEntity._entityName.Add(202, HtmlEntity.getString_0(107244974));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244965), 203);
			HtmlEntity._entityName.Add(203, HtmlEntity.getString_0(107244965));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244924), 204);
			HtmlEntity._entityName.Add(204, HtmlEntity.getString_0(107244924));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244947), 205);
			HtmlEntity._entityName.Add(205, HtmlEntity.getString_0(107244947));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244938), 206);
			HtmlEntity._entityName.Add(206, HtmlEntity.getString_0(107244938));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244897), 207);
			HtmlEntity._entityName.Add(207, HtmlEntity.getString_0(107244897));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244888), 208);
			HtmlEntity._entityName.Add(208, HtmlEntity.getString_0(107244888));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244915), 209);
			HtmlEntity._entityName.Add(209, HtmlEntity.getString_0(107244915));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244906), 210);
			HtmlEntity._entityName.Add(210, HtmlEntity.getString_0(107244906));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244865), 211);
			HtmlEntity._entityName.Add(211, HtmlEntity.getString_0(107244865));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244856), 212);
			HtmlEntity._entityName.Add(212, HtmlEntity.getString_0(107244856));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244879), 213);
			HtmlEntity._entityName.Add(213, HtmlEntity.getString_0(107244879));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244870), 214);
			HtmlEntity._entityName.Add(214, HtmlEntity.getString_0(107244870));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244829), 215);
			HtmlEntity._entityName.Add(215, HtmlEntity.getString_0(107244829));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244820), 216);
			HtmlEntity._entityName.Add(216, HtmlEntity.getString_0(107244820));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244843), 217);
			HtmlEntity._entityName.Add(217, HtmlEntity.getString_0(107244843));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244290), 218);
			HtmlEntity._entityName.Add(218, HtmlEntity.getString_0(107244290));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244281), 219);
			HtmlEntity._entityName.Add(219, HtmlEntity.getString_0(107244281));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244304), 220);
			HtmlEntity._entityName.Add(220, HtmlEntity.getString_0(107244304));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244295), 221);
			HtmlEntity._entityName.Add(221, HtmlEntity.getString_0(107244295));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244254), 222);
			HtmlEntity._entityName.Add(222, HtmlEntity.getString_0(107244254));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244245), 223);
			HtmlEntity._entityName.Add(223, HtmlEntity.getString_0(107244245));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244268), 224);
			HtmlEntity._entityName.Add(224, HtmlEntity.getString_0(107244268));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244227), 225);
			HtmlEntity._entityName.Add(225, HtmlEntity.getString_0(107244227));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244218), 226);
			HtmlEntity._entityName.Add(226, HtmlEntity.getString_0(107244218));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244241), 227);
			HtmlEntity._entityName.Add(227, HtmlEntity.getString_0(107244241));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244232), 228);
			HtmlEntity._entityName.Add(228, HtmlEntity.getString_0(107244232));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244191), 229);
			HtmlEntity._entityName.Add(229, HtmlEntity.getString_0(107244191));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244182), 230);
			HtmlEntity._entityName.Add(230, HtmlEntity.getString_0(107244182));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244205), 231);
			HtmlEntity._entityName.Add(231, HtmlEntity.getString_0(107244205));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244196), 232);
			HtmlEntity._entityName.Add(232, HtmlEntity.getString_0(107244196));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244155), 233);
			HtmlEntity._entityName.Add(233, HtmlEntity.getString_0(107244155));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244178), 234);
			HtmlEntity._entityName.Add(234, HtmlEntity.getString_0(107244178));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244169), 235);
			HtmlEntity._entityName.Add(235, HtmlEntity.getString_0(107244169));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244128), 236);
			HtmlEntity._entityName.Add(236, HtmlEntity.getString_0(107244128));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244119), 237);
			HtmlEntity._entityName.Add(237, HtmlEntity.getString_0(107244119));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244142), 238);
			HtmlEntity._entityName.Add(238, HtmlEntity.getString_0(107244142));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244133), 239);
			HtmlEntity._entityName.Add(239, HtmlEntity.getString_0(107244133));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244092), 240);
			HtmlEntity._entityName.Add(240, HtmlEntity.getString_0(107244092));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244087), 241);
			HtmlEntity._entityName.Add(241, HtmlEntity.getString_0(107244087));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244110), 242);
			HtmlEntity._entityName.Add(242, HtmlEntity.getString_0(107244110));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244101), 243);
			HtmlEntity._entityName.Add(243, HtmlEntity.getString_0(107244101));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244060), 244);
			HtmlEntity._entityName.Add(244, HtmlEntity.getString_0(107244060));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244083), 245);
			HtmlEntity._entityName.Add(245, HtmlEntity.getString_0(107244083));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244074), 246);
			HtmlEntity._entityName.Add(246, HtmlEntity.getString_0(107244074));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244545), 247);
			HtmlEntity._entityName.Add(247, HtmlEntity.getString_0(107244545));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244536), 248);
			HtmlEntity._entityName.Add(248, HtmlEntity.getString_0(107244536));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244559), 249);
			HtmlEntity._entityName.Add(249, HtmlEntity.getString_0(107244559));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244550), 250);
			HtmlEntity._entityName.Add(250, HtmlEntity.getString_0(107244550));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244509), 251);
			HtmlEntity._entityName.Add(251, HtmlEntity.getString_0(107244509));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244500), 252);
			HtmlEntity._entityName.Add(252, HtmlEntity.getString_0(107244500));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244523), 253);
			HtmlEntity._entityName.Add(253, HtmlEntity.getString_0(107244523));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244482), 254);
			HtmlEntity._entityName.Add(254, HtmlEntity.getString_0(107244482));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244473), 255);
			HtmlEntity._entityName.Add(255, HtmlEntity.getString_0(107244473));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244496), 402);
			HtmlEntity._entityName.Add(402, HtmlEntity.getString_0(107244496));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244487), 913);
			HtmlEntity._entityName.Add(913, HtmlEntity.getString_0(107244487));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244446), 914);
			HtmlEntity._entityName.Add(914, HtmlEntity.getString_0(107244446));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244437), 915);
			HtmlEntity._entityName.Add(915, HtmlEntity.getString_0(107244437));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244460), 916);
			HtmlEntity._entityName.Add(916, HtmlEntity.getString_0(107244460));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244419), 917);
			HtmlEntity._entityName.Add(917, HtmlEntity.getString_0(107244419));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244406), 918);
			HtmlEntity._entityName.Add(918, HtmlEntity.getString_0(107244406));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244429), 919);
			HtmlEntity._entityName.Add(919, HtmlEntity.getString_0(107244429));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244424), 920);
			HtmlEntity._entityName.Add(920, HtmlEntity.getString_0(107244424));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244383), 921);
			HtmlEntity._entityName.Add(921, HtmlEntity.getString_0(107244383));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244374), 922);
			HtmlEntity._entityName.Add(922, HtmlEntity.getString_0(107244374));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244397), 923);
			HtmlEntity._entityName.Add(923, HtmlEntity.getString_0(107244397));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244388), 924);
			HtmlEntity._entityName.Add(924, HtmlEntity.getString_0(107244388));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244351), 925);
			HtmlEntity._entityName.Add(925, HtmlEntity.getString_0(107244351));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244346), 926);
			HtmlEntity._entityName.Add(926, HtmlEntity.getString_0(107244346));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244341), 927);
			HtmlEntity._entityName.Add(927, HtmlEntity.getString_0(107244341));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244360), 928);
			HtmlEntity._entityName.Add(928, HtmlEntity.getString_0(107244360));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244323), 929);
			HtmlEntity._entityName.Add(929, HtmlEntity.getString_0(107244323));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244318), 931);
			HtmlEntity._entityName.Add(931, HtmlEntity.getString_0(107244318));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244309), 932);
			HtmlEntity._entityName.Add(932, HtmlEntity.getString_0(107244309));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244336), 933);
			HtmlEntity._entityName.Add(933, HtmlEntity.getString_0(107244336));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243779), 934);
			HtmlEntity._entityName.Add(934, HtmlEntity.getString_0(107243779));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243774), 935);
			HtmlEntity._entityName.Add(935, HtmlEntity.getString_0(107243774));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243769), 936);
			HtmlEntity._entityName.Add(936, HtmlEntity.getString_0(107243769));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243764), 937);
			HtmlEntity._entityName.Add(937, HtmlEntity.getString_0(107243764));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243787), 945);
			HtmlEntity._entityName.Add(945, HtmlEntity.getString_0(107243787));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243746), 946);
			HtmlEntity._entityName.Add(946, HtmlEntity.getString_0(107243746));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243737), 947);
			HtmlEntity._entityName.Add(947, HtmlEntity.getString_0(107243737));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243760), 948);
			HtmlEntity._entityName.Add(948, HtmlEntity.getString_0(107243760));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243751), 949);
			HtmlEntity._entityName.Add(949, HtmlEntity.getString_0(107243751));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243706), 950);
			HtmlEntity._entityName.Add(950, HtmlEntity.getString_0(107243706));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243729), 951);
			HtmlEntity._entityName.Add(951, HtmlEntity.getString_0(107243729));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243724), 952);
			HtmlEntity._entityName.Add(952, HtmlEntity.getString_0(107243724));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243683), 953);
			HtmlEntity._entityName.Add(953, HtmlEntity.getString_0(107243683));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243674), 954);
			HtmlEntity._entityName.Add(954, HtmlEntity.getString_0(107243674));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243697), 955);
			HtmlEntity._entityName.Add(955, HtmlEntity.getString_0(107243697));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243688), 956);
			HtmlEntity._entityName.Add(956, HtmlEntity.getString_0(107243688));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243651), 957);
			HtmlEntity._entityName.Add(957, HtmlEntity.getString_0(107243651));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243646), 958);
			HtmlEntity._entityName.Add(958, HtmlEntity.getString_0(107243646));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243641), 959);
			HtmlEntity._entityName.Add(959, HtmlEntity.getString_0(107243641));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243660), 960);
			HtmlEntity._entityName.Add(960, HtmlEntity.getString_0(107243660));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243655), 961);
			HtmlEntity._entityName.Add(961, HtmlEntity.getString_0(107243655));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243618), 962);
			HtmlEntity._entityName.Add(962, HtmlEntity.getString_0(107243618));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243609), 963);
			HtmlEntity._entityName.Add(963, HtmlEntity.getString_0(107243609));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243632), 964);
			HtmlEntity._entityName.Add(964, HtmlEntity.getString_0(107243632));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243627), 965);
			HtmlEntity._entityName.Add(965, HtmlEntity.getString_0(107243627));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243582), 966);
			HtmlEntity._entityName.Add(966, HtmlEntity.getString_0(107243582));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243577), 967);
			HtmlEntity._entityName.Add(967, HtmlEntity.getString_0(107243577));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243572), 968);
			HtmlEntity._entityName.Add(968, HtmlEntity.getString_0(107243572));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243599), 969);
			HtmlEntity._entityName.Add(969, HtmlEntity.getString_0(107243599));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243590), 977);
			HtmlEntity._entityName.Add(977, HtmlEntity.getString_0(107243590));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243545), 978);
			HtmlEntity._entityName.Add(978, HtmlEntity.getString_0(107243545));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243568), 982);
			HtmlEntity._entityName.Add(982, HtmlEntity.getString_0(107243568));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243563), 8226);
			HtmlEntity._entityName.Add(8226, HtmlEntity.getString_0(107243563));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244034), 8230);
			HtmlEntity._entityName.Add(8230, HtmlEntity.getString_0(107244034));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244025), 8242);
			HtmlEntity._entityName.Add(8242, HtmlEntity.getString_0(107244025));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107454732), 8243);
			HtmlEntity._entityName.Add(8243, HtmlEntity.getString_0(107454732));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244048), 8254);
			HtmlEntity._entityName.Add(8254, HtmlEntity.getString_0(107244048));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244039), 8260);
			HtmlEntity._entityName.Add(8260, HtmlEntity.getString_0(107244039));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243998), 8472);
			HtmlEntity._entityName.Add(8472, HtmlEntity.getString_0(107243998));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107230699), 8465);
			HtmlEntity._entityName.Add(8465, HtmlEntity.getString_0(107230699));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243989), 8476);
			HtmlEntity._entityName.Add(8476, HtmlEntity.getString_0(107243989));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107244012), 8482);
			HtmlEntity._entityName.Add(8482, HtmlEntity.getString_0(107244012));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243971), 8501);
			HtmlEntity._entityName.Add(8501, HtmlEntity.getString_0(107243971));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243958), 8592);
			HtmlEntity._entityName.Add(8592, HtmlEntity.getString_0(107243958));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243981), 8593);
			HtmlEntity._entityName.Add(8593, HtmlEntity.getString_0(107243981));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243972), 8594);
			HtmlEntity._entityName.Add(8594, HtmlEntity.getString_0(107243972));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243931), 8595);
			HtmlEntity._entityName.Add(8595, HtmlEntity.getString_0(107243931));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243954), 8596);
			HtmlEntity._entityName.Add(8596, HtmlEntity.getString_0(107243954));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243945), 8629);
			HtmlEntity._entityName.Add(8629, HtmlEntity.getString_0(107243945));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243904), 8656);
			HtmlEntity._entityName.Add(8656, HtmlEntity.getString_0(107243904));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243895), 8657);
			HtmlEntity._entityName.Add(8657, HtmlEntity.getString_0(107243895));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243918), 8658);
			HtmlEntity._entityName.Add(8658, HtmlEntity.getString_0(107243918));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243909), 8659);
			HtmlEntity._entityName.Add(8659, HtmlEntity.getString_0(107243909));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243868), 8660);
			HtmlEntity._entityName.Add(8660, HtmlEntity.getString_0(107243868));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243891), 8704);
			HtmlEntity._entityName.Add(8704, HtmlEntity.getString_0(107243891));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243882), 8706);
			HtmlEntity._entityName.Add(8706, HtmlEntity.getString_0(107243882));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243841), 8707);
			HtmlEntity._entityName.Add(8707, HtmlEntity.getString_0(107243841));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243832), 8709);
			HtmlEntity._entityName.Add(8709, HtmlEntity.getString_0(107243832));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243855), 8711);
			HtmlEntity._entityName.Add(8711, HtmlEntity.getString_0(107243855));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243846), 8712);
			HtmlEntity._entityName.Add(8712, HtmlEntity.getString_0(107243846));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243805), 8713);
			HtmlEntity._entityName.Add(8713, HtmlEntity.getString_0(107243805));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243796), 8715);
			HtmlEntity._entityName.Add(8715, HtmlEntity.getString_0(107243796));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243823), 8719);
			HtmlEntity._entityName.Add(8719, HtmlEntity.getString_0(107243823));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243814), 8721);
			HtmlEntity._entityName.Add(8721, HtmlEntity.getString_0(107243814));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243265), 8722);
			HtmlEntity._entityName.Add(8722, HtmlEntity.getString_0(107243265));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243256), 8727);
			HtmlEntity._entityName.Add(8727, HtmlEntity.getString_0(107243256));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243279), 8730);
			HtmlEntity._entityName.Add(8730, HtmlEntity.getString_0(107243279));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243270), 8733);
			HtmlEntity._entityName.Add(8733, HtmlEntity.getString_0(107243270));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243229), 8734);
			HtmlEntity._entityName.Add(8734, HtmlEntity.getString_0(107243229));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243220), 8736);
			HtmlEntity._entityName.Add(8736, HtmlEntity.getString_0(107243220));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107388530), 8743);
			HtmlEntity._entityName.Add(8743, HtmlEntity.getString_0(107388530));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243247), 8744);
			HtmlEntity._entityName.Add(8744, HtmlEntity.getString_0(107243247));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243242), 8745);
			HtmlEntity._entityName.Add(8745, HtmlEntity.getString_0(107243242));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243237), 8746);
			HtmlEntity._entityName.Add(8746, HtmlEntity.getString_0(107243237));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243200), 8747);
			HtmlEntity._entityName.Add(8747, HtmlEntity.getString_0(107243200));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243195), 8756);
			HtmlEntity._entityName.Add(8756, HtmlEntity.getString_0(107243195));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243218), 8764);
			HtmlEntity._entityName.Add(8764, HtmlEntity.getString_0(107243218));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243213), 8773);
			HtmlEntity._entityName.Add(8773, HtmlEntity.getString_0(107243213));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243204), 8776);
			HtmlEntity._entityName.Add(8776, HtmlEntity.getString_0(107243204));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243163), 8800);
			HtmlEntity._entityName.Add(8800, HtmlEntity.getString_0(107243163));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243158), 8801);
			HtmlEntity._entityName.Add(8801, HtmlEntity.getString_0(107243158));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243181), 8804);
			HtmlEntity._entityName.Add(8804, HtmlEntity.getString_0(107243181));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243176), 8805);
			HtmlEntity._entityName.Add(8805, HtmlEntity.getString_0(107243176));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243139), 8834);
			HtmlEntity._entityName.Add(8834, HtmlEntity.getString_0(107243139));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243134), 8835);
			HtmlEntity._entityName.Add(8835, HtmlEntity.getString_0(107243134));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243129), 8836);
			HtmlEntity._entityName.Add(8836, HtmlEntity.getString_0(107243129));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243152), 8838);
			HtmlEntity._entityName.Add(8838, HtmlEntity.getString_0(107243152));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243143), 8839);
			HtmlEntity._entityName.Add(8839, HtmlEntity.getString_0(107243143));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243102), 8853);
			HtmlEntity._entityName.Add(8853, HtmlEntity.getString_0(107243102));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243093), 8855);
			HtmlEntity._entityName.Add(8855, HtmlEntity.getString_0(107243093));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243116), 8869);
			HtmlEntity._entityName.Add(8869, HtmlEntity.getString_0(107243116));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243075), 8901);
			HtmlEntity._entityName.Add(8901, HtmlEntity.getString_0(107243075));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243066), 8968);
			HtmlEntity._entityName.Add(8968, HtmlEntity.getString_0(107243066));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243089), 8969);
			HtmlEntity._entityName.Add(8969, HtmlEntity.getString_0(107243089));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243080), 8970);
			HtmlEntity._entityName.Add(8970, HtmlEntity.getString_0(107243080));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243039), 8971);
			HtmlEntity._entityName.Add(8971, HtmlEntity.getString_0(107243039));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243030), 9001);
			HtmlEntity._entityName.Add(9001, HtmlEntity.getString_0(107243030));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243053), 9002);
			HtmlEntity._entityName.Add(9002, HtmlEntity.getString_0(107243053));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243044), 9674);
			HtmlEntity._entityName.Add(9674, HtmlEntity.getString_0(107243044));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243519), 9824);
			HtmlEntity._entityName.Add(9824, HtmlEntity.getString_0(107243519));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243510), 9827);
			HtmlEntity._entityName.Add(9827, HtmlEntity.getString_0(107243510));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243533), 9829);
			HtmlEntity._entityName.Add(9829, HtmlEntity.getString_0(107243533));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243524), 9830);
			HtmlEntity._entityName.Add(9830, HtmlEntity.getString_0(107243524));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243483), 338);
			HtmlEntity._entityName.Add(338, HtmlEntity.getString_0(107243483));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243506), 339);
			HtmlEntity._entityName.Add(339, HtmlEntity.getString_0(107243506));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243497), 352);
			HtmlEntity._entityName.Add(352, HtmlEntity.getString_0(107243497));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243456), 353);
			HtmlEntity._entityName.Add(353, HtmlEntity.getString_0(107243456));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243447), 376);
			HtmlEntity._entityName.Add(376, HtmlEntity.getString_0(107243447));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243470), 710);
			HtmlEntity._entityName.Add(710, HtmlEntity.getString_0(107243470));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243461), 732);
			HtmlEntity._entityName.Add(732, HtmlEntity.getString_0(107243461));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243420), 8194);
			HtmlEntity._entityName.Add(8194, HtmlEntity.getString_0(107243420));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243443), 8195);
			HtmlEntity._entityName.Add(8195, HtmlEntity.getString_0(107243443));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243434), 8201);
			HtmlEntity._entityName.Add(8201, HtmlEntity.getString_0(107243434));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243393), 8204);
			HtmlEntity._entityName.Add(8204, HtmlEntity.getString_0(107243393));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243384), 8205);
			HtmlEntity._entityName.Add(8205, HtmlEntity.getString_0(107243384));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243411), 8206);
			HtmlEntity._entityName.Add(8206, HtmlEntity.getString_0(107243411));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243406), 8207);
			HtmlEntity._entityName.Add(8207, HtmlEntity.getString_0(107243406));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243401), 8211);
			HtmlEntity._entityName.Add(8211, HtmlEntity.getString_0(107243401));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243360), 8212);
			HtmlEntity._entityName.Add(8212, HtmlEntity.getString_0(107243360));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243351), 8216);
			HtmlEntity._entityName.Add(8216, HtmlEntity.getString_0(107243351));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243374), 8217);
			HtmlEntity._entityName.Add(8217, HtmlEntity.getString_0(107243374));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243365), 8218);
			HtmlEntity._entityName.Add(8218, HtmlEntity.getString_0(107243365));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243324), 8220);
			HtmlEntity._entityName.Add(8220, HtmlEntity.getString_0(107243324));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243347), 8221);
			HtmlEntity._entityName.Add(8221, HtmlEntity.getString_0(107243347));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243338), 8222);
			HtmlEntity._entityName.Add(8222, HtmlEntity.getString_0(107243338));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243297), 8224);
			HtmlEntity._entityName.Add(8224, HtmlEntity.getString_0(107243297));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107448005), 8225);
			HtmlEntity._entityName.Add(8225, HtmlEntity.getString_0(107448005));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243288), 8240);
			HtmlEntity._entityName.Add(8240, HtmlEntity.getString_0(107243288));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243311), 8249);
			HtmlEntity._entityName.Add(8249, HtmlEntity.getString_0(107243311));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107243302), 8250);
			HtmlEntity._entityName.Add(8250, HtmlEntity.getString_0(107243302));
			HtmlEntity._entityValue.Add(HtmlEntity.getString_0(107242749), 8364);
			HtmlEntity._entityName.Add(8364, HtmlEntity.getString_0(107242749));
			HtmlEntity._maxEntitySize = 9;
		}

		private HtmlEntity()
		{
		}

		public static string DeEntitize(string text)
		{
			if (text == null)
			{
				return null;
			}
			if (text.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			HtmlEntity.ParseState parseState = HtmlEntity.ParseState.Text;
			StringBuilder stringBuilder2 = new StringBuilder(10);
			for (int i = 0; i < text.Length; i++)
			{
				if (parseState != HtmlEntity.ParseState.Text)
				{
					if (parseState == HtmlEntity.ParseState.EntityStart)
					{
						char c = text[i];
						if (c != '&')
						{
							if (c == ';')
							{
								if (stringBuilder2.Length == 0)
								{
									stringBuilder.Append(HtmlEntity.getString_0(107242740));
								}
								else
								{
									if (stringBuilder2[0] == '#')
									{
										string text2 = stringBuilder2.ToString();
										try
										{
											string text3 = text2.Substring(1).Trim();
											int fromBase;
											if (text3.StartsWith(HtmlEntity.getString_0(107399905), StringComparison.OrdinalIgnoreCase))
											{
												fromBase = 16;
												text3 = text3.Substring(1);
											}
											else
											{
												fromBase = 10;
											}
											int value = Convert.ToInt32(text3, fromBase);
											stringBuilder.Append(Convert.ToChar(value));
											goto IL_242;
										}
										catch
										{
											stringBuilder.Append(HtmlEntity.getString_0(107242767) + text2 + HtmlEntity.getString_0(107242762));
											goto IL_242;
										}
										goto IL_122;
									}
									goto IL_122;
									IL_242:
									stringBuilder2.Remove(0, stringBuilder2.Length);
									goto IL_185;
									IL_122:
									int value2;
									if (!HtmlEntity._entityValue.TryGetValue(stringBuilder2.ToString(), out value2))
									{
										StringBuilder stringBuilder3 = stringBuilder;
										string str = HtmlEntity.getString_0(107455085);
										StringBuilder stringBuilder4 = stringBuilder2;
										stringBuilder3.Append(str + ((stringBuilder4 != null) ? stringBuilder4.ToString() : null) + HtmlEntity.getString_0(107242762));
										goto IL_242;
									}
									stringBuilder.Append(Convert.ToChar(value2));
									goto IL_242;
								}
								IL_185:
								parseState = HtmlEntity.ParseState.Text;
							}
							else
							{
								stringBuilder2.Append(text[i]);
								if (stringBuilder2.Length > HtmlEntity._maxEntitySize)
								{
									parseState = HtmlEntity.ParseState.Text;
									StringBuilder stringBuilder5 = stringBuilder;
									string str2 = HtmlEntity.getString_0(107455085);
									StringBuilder stringBuilder6 = stringBuilder2;
									stringBuilder5.Append(str2 + ((stringBuilder6 != null) ? stringBuilder6.ToString() : null));
									stringBuilder2.Remove(0, stringBuilder2.Length);
								}
							}
						}
						else
						{
							StringBuilder stringBuilder7 = stringBuilder;
							string str3 = HtmlEntity.getString_0(107455085);
							StringBuilder stringBuilder8 = stringBuilder2;
							stringBuilder7.Append(str3 + ((stringBuilder8 != null) ? stringBuilder8.ToString() : null));
							stringBuilder2.Remove(0, stringBuilder2.Length);
						}
					}
				}
				else if (text[i] == '&')
				{
					parseState = HtmlEntity.ParseState.EntityStart;
				}
				else
				{
					stringBuilder.Append(text[i]);
				}
			}
			if (parseState == HtmlEntity.ParseState.EntityStart)
			{
				StringBuilder stringBuilder9 = stringBuilder;
				string str4 = HtmlEntity.getString_0(107455085);
				StringBuilder stringBuilder10 = stringBuilder2;
				stringBuilder9.Append(str4 + ((stringBuilder10 != null) ? stringBuilder10.ToString() : null));
			}
			return stringBuilder.ToString();
		}

		public static HtmlNode Entitize(HtmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException(HtmlEntity.getString_0(107242757));
			}
			HtmlNode htmlNode = node.CloneNode(true);
			if (htmlNode.HasAttributes)
			{
				HtmlEntity.Entitize(htmlNode.Attributes);
			}
			if (htmlNode.HasChildNodes)
			{
				HtmlEntity.Entitize(htmlNode.ChildNodes);
			}
			else if (htmlNode.NodeType == HtmlNodeType.Text)
			{
				((HtmlTextNode)htmlNode).Text = HtmlEntity.Entitize(((HtmlTextNode)htmlNode).Text, true, true);
			}
			return htmlNode;
		}

		public static string Entitize(string text)
		{
			return HtmlEntity.Entitize(text, true);
		}

		public static string Entitize(string text, bool useNames)
		{
			return HtmlEntity.Entitize(text, useNames, false);
		}

		public static string Entitize(string text, bool useNames, bool entitizeQuotAmpAndLtGt)
		{
			if (text == null)
			{
				return null;
			}
			if (text.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			if (HtmlEntity.UseWebUtility)
			{
				TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(text);
				while (textElementEnumerator.MoveNext())
				{
					stringBuilder.Append(WebUtility.HtmlEncode(textElementEnumerator.GetTextElement()));
				}
			}
			else
			{
				int i = 0;
				while (i < text.Length)
				{
					int num = (int)text[i];
					if (num > 127)
					{
						goto IL_87;
					}
					if (entitizeQuotAmpAndLtGt)
					{
						if (num == 34 || num == 38 || num == 60)
						{
							goto IL_87;
						}
						if (num == 62)
						{
							goto IL_87;
						}
					}
					stringBuilder.Append(text[i]);
					IL_FE:
					i++;
					continue;
					IL_87:
					string text2 = null;
					if (useNames)
					{
						HtmlEntity.EntityName.TryGetValue(num, out text2);
					}
					if (text2 == null)
					{
						stringBuilder.Append(HtmlEntity.getString_0(107242767) + num.ToString() + HtmlEntity.getString_0(107242762));
						goto IL_FE;
					}
					stringBuilder.Append(HtmlEntity.getString_0(107455085) + text2 + HtmlEntity.getString_0(107242762));
					goto IL_FE;
				}
			}
			return stringBuilder.ToString();
		}

		private static void Entitize(HtmlAttributeCollection collection)
		{
			foreach (HtmlAttribute htmlAttribute in ((IEnumerable<HtmlAttribute>)collection))
			{
				if (htmlAttribute.Value != null)
				{
					htmlAttribute.Value = HtmlEntity.Entitize(htmlAttribute.Value);
				}
			}
		}

		private static void Entitize(HtmlNodeCollection collection)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)collection))
			{
				if (htmlNode.HasAttributes)
				{
					HtmlEntity.Entitize(htmlNode.Attributes);
				}
				if (htmlNode.HasChildNodes)
				{
					HtmlEntity.Entitize(htmlNode.ChildNodes);
				}
				else if (htmlNode.NodeType == HtmlNodeType.Text)
				{
					((HtmlTextNode)htmlNode).Text = HtmlEntity.Entitize(((HtmlTextNode)htmlNode).Text, true, true);
				}
			}
		}

		private static readonly int _maxEntitySize;

		private static Dictionary<int, string> _entityName;

		private static Dictionary<string, int> _entityValue;

		[NonSerialized]
		internal static GetString getString_0;

		private enum ParseState
		{
			Text,
			EntityStart
		}
	}
}
