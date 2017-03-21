namespace ReCodeTest {
	public class TestParser3 {
		public static unsafe int Parse(char * start, int len){
            var pNext = start;
            var pLimit = start + len;
            var pEnd = start; 
	    textMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Sequence(CaseInsensitive,'<--')
 *  +-Accept(1)
 *  |  +-Sequence(CaseInsensitive,'<?')
 *  +-Accept(2)
 *  |  +-Sequence(CaseSensitive,'<![CDATA[')
 *  +-Accept(3)
 *  |  +-Sequence(CaseInsensitive,'<!')
 *  +-Accept(4)
 *  |  +-Sequence(CaseInsensitive,'<script')
 *  +-Accept(5)
 *  |  +-Concat
 *  |     +-Sequence(CaseSensitive,'</')
 *  |     +-Ranges(['a'-'z'],['A'-'Z'])
 *  |     +-Repeat(ZeroOrMore)
 *  |        +-Ranges(['a'-'z'],['A'-'Z'],['0'-'9'],':','-')
 *  +-Accept(6)
 *  |  +-Concat
 *  |     +-Sequence(CaseSensitive,'<')
 *  |     +-Ranges(['a'-'z'],['A'-'Z'])
 *  |     +-Repeat(ZeroOrMore)
 *  |        +-Ranges(['a'-'z'],['A'-'Z'],['0'-'9'],':','-')
 *  +-Accept(7)
 *  |  +-Concat
 *  |     +-Sequence(CaseSensitive,'&#')
 *  |     +-Repeat(OneOrMore)
 *  |     |  +-Ranges(['0'-'9'])
 *  |     +-Sequence(CaseSensitive,';')
 *  +-Accept(8)
 *  |  +-Concat
 *  |     +-Sequence(CaseSensitive,'&#x')
 *  |     +-Repeat(OneOrMore)
 *  |     |  +-Ranges(['0'-'9'],['a'-'f'],['A'-'F'])
 *  |     +-Sequence(CaseSensitive,';')
 *  +-Accept(9)
 *  |  +-Sequence(CaseSensitive,'&quot;')
 *  +-Accept(10)
 *  |  +-Sequence(CaseSensitive,'&amp;')
 *  +-Accept(11)
 *  |  +-Sequence(CaseSensitive,'&lt;')
 *  +-Accept(12)
 *  |  +-Sequence(CaseSensitive,'&gt;')
 *  +-Accept(13)
 *  |  +-Sequence(CaseSensitive,'&Agrave;')
 *  +-Accept(14)
 *  |  +-Sequence(CaseSensitive,'&Aacute;')
 *  +-Accept(15)
 *  |  +-Sequence(CaseSensitive,'&Acirc;')
 *  +-Accept(16)
 *  |  +-Sequence(CaseSensitive,'&Atilde;')
 *  +-Accept(17)
 *  |  +-Sequence(CaseSensitive,'&Auml;')
 *  +-Accept(18)
 *  |  +-Sequence(CaseSensitive,'&Aring;')
 *  +-Accept(19)
 *  |  +-Sequence(CaseSensitive,'&AElig;')
 *  +-Accept(20)
 *  |  +-Sequence(CaseSensitive,'&Ccedil;')
 *  +-Accept(21)
 *  |  +-Sequence(CaseSensitive,'&Egrave;')
 *  +-Accept(22)
 *  |  +-Sequence(CaseSensitive,'&Eacute;')
 *  +-Accept(23)
 *  |  +-Sequence(CaseSensitive,'&Ecirc;')
 *  +-Accept(24)
 *  |  +-Sequence(CaseSensitive,'&Euml;')
 *  +-Accept(25)
 *  |  +-Sequence(CaseSensitive,'&Igrave;')
 *  +-Accept(26)
 *  |  +-Sequence(CaseSensitive,'&Iacute;')
 *  +-Accept(27)
 *  |  +-Sequence(CaseSensitive,'&Icirc;')
 *  +-Accept(28)
 *  |  +-Sequence(CaseSensitive,'&Iuml;')
 *  +-Accept(29)
 *  |  +-Sequence(CaseSensitive,'&ETH;')
 *  +-Accept(30)
 *  |  +-Sequence(CaseSensitive,'&Ntilde;')
 *  +-Accept(31)
 *  |  +-Sequence(CaseSensitive,'&Ograve;')
 *  +-Accept(32)
 *  |  +-Sequence(CaseSensitive,'&Oacute;')
 *  +-Accept(33)
 *  |  +-Sequence(CaseSensitive,'&Ocirc;')
 *  +-Accept(34)
 *  |  +-Sequence(CaseSensitive,'&Otilde;')
 *  +-Accept(35)
 *  |  +-Sequence(CaseSensitive,'&Ouml;')
 *  +-Accept(36)
 *  |  +-Sequence(CaseSensitive,'&Oslash;')
 *  +-Accept(37)
 *  |  +-Sequence(CaseSensitive,'&Ugrave;')
 *  +-Accept(38)
 *  |  +-Sequence(CaseSensitive,'&Uacute;')
 *  +-Accept(39)
 *  |  +-Sequence(CaseSensitive,'&Ucirc;')
 *  +-Accept(40)
 *  |  +-Sequence(CaseSensitive,'&Uuml;')
 *  +-Accept(41)
 *  |  +-Sequence(CaseSensitive,'&Yacute;')
 *  +-Accept(42)
 *  |  +-Sequence(CaseSensitive,'&THORN;')
 *  +-Accept(43)
 *  |  +-Sequence(CaseSensitive,'&szlig;')
 *  +-Accept(44)
 *  |  +-Sequence(CaseSensitive,'&agrave;')
 *  +-Accept(45)
 *  |  +-Sequence(CaseSensitive,'&aacute;')
 *  +-Accept(46)
 *  |  +-Sequence(CaseSensitive,'&acirc;')
 *  +-Accept(47)
 *  |  +-Sequence(CaseSensitive,'&atilde;')
 *  +-Accept(48)
 *  |  +-Sequence(CaseSensitive,'&auml;')
 *  +-Accept(49)
 *  |  +-Sequence(CaseSensitive,'&aring;')
 *  +-Accept(50)
 *  |  +-Sequence(CaseSensitive,'&aelig;')
 *  +-Accept(51)
 *  |  +-Sequence(CaseSensitive,'&ccedil;')
 *  +-Accept(52)
 *  |  +-Sequence(CaseSensitive,'&egrave;')
 *  +-Accept(53)
 *  |  +-Sequence(CaseSensitive,'&eacute;')
 *  +-Accept(54)
 *  |  +-Sequence(CaseSensitive,'&ecirc;')
 *  +-Accept(55)
 *  |  +-Sequence(CaseSensitive,'&euml;')
 *  +-Accept(56)
 *  |  +-Sequence(CaseSensitive,'&igrave;')
 *  +-Accept(57)
 *  |  +-Sequence(CaseSensitive,'&iacute;')
 *  +-Accept(58)
 *  |  +-Sequence(CaseSensitive,'&icirc;')
 *  +-Accept(59)
 *  |  +-Sequence(CaseSensitive,'&iuml;')
 *  +-Accept(60)
 *  |  +-Sequence(CaseSensitive,'&eth;')
 *  +-Accept(61)
 *  |  +-Sequence(CaseSensitive,'&ntilde;')
 *  +-Accept(62)
 *  |  +-Sequence(CaseSensitive,'&ograve;')
 *  +-Accept(63)
 *  |  +-Sequence(CaseSensitive,'&oacute;')
 *  +-Accept(64)
 *  |  +-Sequence(CaseSensitive,'&ocirc;')
 *  +-Accept(65)
 *  |  +-Sequence(CaseSensitive,'&otilde;')
 *  +-Accept(66)
 *  |  +-Sequence(CaseSensitive,'&ouml;')
 *  +-Accept(67)
 *  |  +-Sequence(CaseSensitive,'&oslash;')
 *  +-Accept(68)
 *  |  +-Sequence(CaseSensitive,'&ugrave;')
 *  +-Accept(69)
 *  |  +-Sequence(CaseSensitive,'&uacute;')
 *  +-Accept(70)
 *  |  +-Sequence(CaseSensitive,'&ucirc;')
 *  +-Accept(71)
 *  |  +-Sequence(CaseSensitive,'&uuml;')
 *  +-Accept(72)
 *  |  +-Sequence(CaseSensitive,'&yacute;')
 *  +-Accept(73)
 *  |  +-Sequence(CaseSensitive,'&thorn;')
 *  +-Accept(74)
 *  |  +-Sequence(CaseSensitive,'&yuml;')
 *  +-Accept(75)
 *  |  +-Sequence(CaseSensitive,'&nbsp;')
 *  +-Accept(76)
 *  |  +-Sequence(CaseSensitive,'&iexcl;')
 *  +-Accept(77)
 *  |  +-Sequence(CaseSensitive,'&cent;')
 *  +-Accept(78)
 *  |  +-Sequence(CaseSensitive,'&pound;')
 *  +-Accept(79)
 *  |  +-Sequence(CaseSensitive,'&curren;')
 *  +-Accept(80)
 *  |  +-Sequence(CaseSensitive,'&yen;')
 *  +-Accept(81)
 *  |  +-Sequence(CaseSensitive,'&brvbar;')
 *  +-Accept(82)
 *  |  +-Sequence(CaseSensitive,'&sect;')
 *  +-Accept(83)
 *  |  +-Sequence(CaseSensitive,'&uml;')
 *  +-Accept(84)
 *  |  +-Sequence(CaseSensitive,'&copy;')
 *  +-Accept(85)
 *  |  +-Sequence(CaseSensitive,'&ordf;')
 *  +-Accept(86)
 *  |  +-Sequence(CaseSensitive,'&laquo;')
 *  +-Accept(87)
 *  |  +-Sequence(CaseSensitive,'&not;')
 *  +-Accept(88)
 *  |  +-Sequence(CaseSensitive,'&shy;')
 *  +-Accept(89)
 *  |  +-Sequence(CaseSensitive,'&reg;')
 *  +-Accept(90)
 *  |  +-Sequence(CaseSensitive,'&macr;')
 *  +-Accept(91)
 *  |  +-Sequence(CaseSensitive,'&deg;')
 *  +-Accept(92)
 *  |  +-Sequence(CaseSensitive,'&plusmn;')
 *  +-Accept(93)
 *  |  +-Sequence(CaseSensitive,'&sup2;')
 *  +-Accept(94)
 *  |  +-Sequence(CaseSensitive,'&sup3;')
 *  +-Accept(95)
 *  |  +-Sequence(CaseSensitive,'&acute;')
 *  +-Accept(96)
 *  |  +-Sequence(CaseSensitive,'&micro;')
 *  +-Accept(97)
 *  |  +-Sequence(CaseSensitive,'&para;')
 *  +-Accept(98)
 *  |  +-Sequence(CaseSensitive,'&middot;')
 *  +-Accept(99)
 *  |  +-Sequence(CaseSensitive,'&cedil;')
 *  +-Accept(100)
 *  |  +-Sequence(CaseSensitive,'&sup1;')
 *  +-Accept(101)
 *  |  +-Sequence(CaseSensitive,'&ordm;')
 *  +-Accept(102)
 *  |  +-Sequence(CaseSensitive,'&raquo;')
 *  +-Accept(103)
 *  |  +-Sequence(CaseSensitive,'&frac14;')
 *  +-Accept(104)
 *  |  +-Sequence(CaseSensitive,'&frac12;')
 *  +-Accept(105)
 *  |  +-Sequence(CaseSensitive,'&frac34;')
 *  +-Accept(106)
 *  |  +-Sequence(CaseSensitive,'&iquest;')
 *  +-Accept(107)
 *  |  +-Sequence(CaseSensitive,'&times;')
 *  +-Accept(108)
 *  |  +-Sequence(CaseSensitive,'&divide;')
 *  +-Accept(109)
 *  |  +-Sequence(CaseSensitive,'&Alpha;')
 *  +-Accept(110)
 *  |  +-Sequence(CaseSensitive,'&Beta;')
 *  +-Accept(111)
 *  |  +-Sequence(CaseSensitive,'&Gamma;')
 *  +-Accept(112)
 *  |  +-Sequence(CaseSensitive,'&Delta;')
 *  +-Accept(113)
 *  |  +-Sequence(CaseSensitive,'&Epsilon;')
 *  +-Accept(114)
 *  |  +-Sequence(CaseSensitive,'&Zeta;')
 *  +-Accept(115)
 *  |  +-Sequence(CaseSensitive,'&Eta;')
 *  +-Accept(116)
 *  |  +-Sequence(CaseSensitive,'&Theta;')
 *  +-Accept(117)
 *  |  +-Sequence(CaseSensitive,'&Iota;')
 *  +-Accept(118)
 *  |  +-Sequence(CaseSensitive,'&Kappa;')
 *  +-Accept(119)
 *  |  +-Sequence(CaseSensitive,'&Lambda;')
 *  +-Accept(120)
 *  |  +-Sequence(CaseSensitive,'&Mu;')
 *  +-Accept(121)
 *  |  +-Sequence(CaseSensitive,'&Nu;')
 *  +-Accept(122)
 *  |  +-Sequence(CaseSensitive,'&Xi;')
 *  +-Accept(123)
 *  |  +-Sequence(CaseSensitive,'&Omicron;')
 *  +-Accept(124)
 *  |  +-Sequence(CaseSensitive,'&Pi;')
 *  +-Accept(125)
 *  |  +-Sequence(CaseSensitive,'&Rho;')
 *  +-Accept(126)
 *  |  +-Sequence(CaseSensitive,'&Sigma;')
 *  +-Accept(127)
 *  |  +-Sequence(CaseSensitive,'&Tau;')
 *  +-Accept(128)
 *  |  +-Sequence(CaseSensitive,'&Upsilon;')
 *  +-Accept(129)
 *  |  +-Sequence(CaseSensitive,'&Phi;')
 *  +-Accept(130)
 *  |  +-Sequence(CaseSensitive,'&Chi;')
 *  +-Accept(131)
 *  |  +-Sequence(CaseSensitive,'&Psi;')
 *  +-Accept(132)
 *  |  +-Sequence(CaseSensitive,'&Omega;')
 *  +-Accept(133)
 *  |  +-Sequence(CaseSensitive,'&alpha;')
 *  +-Accept(134)
 *  |  +-Sequence(CaseSensitive,'&beta;')
 *  +-Accept(135)
 *  |  +-Sequence(CaseSensitive,'&gamma;')
 *  +-Accept(136)
 *  |  +-Sequence(CaseSensitive,'&delta;')
 *  +-Accept(137)
 *  |  +-Sequence(CaseSensitive,'&epsilon;')
 *  +-Accept(138)
 *  |  +-Sequence(CaseSensitive,'&zeta;')
 *  +-Accept(139)
 *  |  +-Sequence(CaseSensitive,'&eta;')
 *  +-Accept(140)
 *  |  +-Sequence(CaseSensitive,'&theta;')
 *  +-Accept(141)
 *  |  +-Sequence(CaseSensitive,'&iota;')
 *  +-Accept(142)
 *  |  +-Sequence(CaseSensitive,'&kappa;')
 *  +-Accept(143)
 *  |  +-Sequence(CaseSensitive,'&lambda;')
 *  +-Accept(144)
 *  |  +-Sequence(CaseSensitive,'&mu;')
 *  +-Accept(145)
 *  |  +-Sequence(CaseSensitive,'&nu;')
 *  +-Accept(146)
 *  |  +-Sequence(CaseSensitive,'&xi;')
 *  +-Accept(147)
 *  |  +-Sequence(CaseSensitive,'&omicron;')
 *  +-Accept(148)
 *  |  +-Sequence(CaseSensitive,'&pi;')
 *  +-Accept(149)
 *  |  +-Sequence(CaseSensitive,'&rho;')
 *  +-Accept(150)
 *  |  +-Sequence(CaseSensitive,'&sigmaf;')
 *  +-Accept(151)
 *  |  +-Sequence(CaseSensitive,'&sigma;')
 *  +-Accept(152)
 *  |  +-Sequence(CaseSensitive,'&tau;')
 *  +-Accept(153)
 *  |  +-Sequence(CaseSensitive,'&upsilon;')
 *  +-Accept(154)
 *  |  +-Sequence(CaseSensitive,'&phi;')
 *  +-Accept(155)
 *  |  +-Sequence(CaseSensitive,'&chi;')
 *  +-Accept(156)
 *  |  +-Sequence(CaseSensitive,'&psi;')
 *  +-Accept(157)
 *  |  +-Sequence(CaseSensitive,'&omega;')
 *  +-Accept(158)
 *  |  +-Sequence(CaseSensitive,'&thetasym;')
 *  +-Accept(159)
 *  |  +-Sequence(CaseSensitive,'&upsih;')
 *  +-Accept(160)
 *  |  +-Sequence(CaseSensitive,'&piv;')
 *  +-Accept(161)
 *  |  +-Sequence(CaseSensitive,'&forall;')
 *  +-Accept(162)
 *  |  +-Sequence(CaseSensitive,'&part;')
 *  +-Accept(163)
 *  |  +-Sequence(CaseSensitive,'&exist;')
 *  +-Accept(164)
 *  |  +-Sequence(CaseSensitive,'&empty;')
 *  +-Accept(165)
 *  |  +-Sequence(CaseSensitive,'&nabla;')
 *  +-Accept(166)
 *  |  +-Sequence(CaseSensitive,'&isin;')
 *  +-Accept(167)
 *  |  +-Sequence(CaseSensitive,'&notin;')
 *  +-Accept(168)
 *  |  +-Sequence(CaseSensitive,'&ni;')
 *  +-Accept(169)
 *  |  +-Sequence(CaseSensitive,'&prod;')
 *  +-Accept(170)
 *  |  +-Sequence(CaseSensitive,'&sum;')
 *  +-Accept(171)
 *  |  +-Sequence(CaseSensitive,'&minus;')
 *  +-Accept(172)
 *  |  +-Sequence(CaseSensitive,'&lowast;')
 *  +-Accept(173)
 *  |  +-Sequence(CaseSensitive,'&radic;')
 *  +-Accept(174)
 *  |  +-Sequence(CaseSensitive,'&prop;')
 *  +-Accept(175)
 *  |  +-Sequence(CaseSensitive,'&infin;')
 *  +-Accept(176)
 *  |  +-Sequence(CaseSensitive,'&ang;')
 *  +-Accept(177)
 *  |  +-Sequence(CaseSensitive,'&and;')
 *  +-Accept(178)
 *  |  +-Sequence(CaseSensitive,'&or;')
 *  +-Accept(179)
 *  |  +-Sequence(CaseSensitive,'&cap;')
 *  +-Accept(180)
 *  |  +-Sequence(CaseSensitive,'&cup;')
 *  +-Accept(181)
 *  |  +-Sequence(CaseSensitive,'&int;')
 *  +-Accept(182)
 *  |  +-Sequence(CaseSensitive,'&there4;')
 *  +-Accept(183)
 *  |  +-Sequence(CaseSensitive,'&sim;')
 *  +-Accept(184)
 *  |  +-Sequence(CaseSensitive,'&cong;')
 *  +-Accept(185)
 *  |  +-Sequence(CaseSensitive,'&asymp;')
 *  +-Accept(186)
 *  |  +-Sequence(CaseSensitive,'&ne;')
 *  +-Accept(187)
 *  |  +-Sequence(CaseSensitive,'&equiv;')
 *  +-Accept(188)
 *  |  +-Sequence(CaseSensitive,'&le;')
 *  +-Accept(189)
 *  |  +-Sequence(CaseSensitive,'&ge;')
 *  +-Accept(190)
 *  |  +-Sequence(CaseSensitive,'&sub;')
 *  +-Accept(191)
 *  |  +-Sequence(CaseSensitive,'&sup;')
 *  +-Accept(192)
 *  |  +-Sequence(CaseSensitive,'&nsub;')
 *  +-Accept(193)
 *  |  +-Sequence(CaseSensitive,'&sube;')
 *  +-Accept(194)
 *  |  +-Sequence(CaseSensitive,'&supe;')
 *  +-Accept(195)
 *  |  +-Sequence(CaseSensitive,'&oplus;')
 *  +-Accept(196)
 *  |  +-Sequence(CaseSensitive,'&otimes;')
 *  +-Accept(197)
 *  |  +-Sequence(CaseSensitive,'&perp;')
 *  +-Accept(198)
 *  |  +-Sequence(CaseSensitive,'&sdot;')
 *  +-Accept(199)
 *  |  +-Sequence(CaseSensitive,'&OElig;')
 *  +-Accept(200)
 *  |  +-Sequence(CaseSensitive,'&oelig;')
 *  +-Accept(201)
 *  |  +-Sequence(CaseSensitive,'&Scaron;')
 *  +-Accept(202)
 *  |  +-Sequence(CaseSensitive,'&scaron;')
 *  +-Accept(203)
 *  |  +-Sequence(CaseSensitive,'&Yuml;')
 *  +-Accept(204)
 *  |  +-Sequence(CaseSensitive,'&fnof;')
 *  +-Accept(205)
 *  |  +-Sequence(CaseSensitive,'&circ;')
 *  +-Accept(206)
 *  |  +-Sequence(CaseSensitive,'&tilde;')
 *  +-Accept(207)
 *  |  +-Sequence(CaseSensitive,'&ensp;')
 *  +-Accept(208)
 *  |  +-Sequence(CaseSensitive,'&emsp;')
 *  +-Accept(209)
 *  |  +-Sequence(CaseSensitive,'&thinsp;')
 *  +-Accept(210)
 *  |  +-Sequence(CaseSensitive,'&zwnj;')
 *  +-Accept(211)
 *  |  +-Sequence(CaseSensitive,'&zwj;')
 *  +-Accept(212)
 *  |  +-Sequence(CaseSensitive,'&lrm;')
 *  +-Accept(213)
 *  |  +-Sequence(CaseSensitive,'&rlm;')
 *  +-Accept(214)
 *  |  +-Sequence(CaseSensitive,'&ndash;')
 *  +-Accept(215)
 *  |  +-Sequence(CaseSensitive,'&mdash;')
 *  +-Accept(216)
 *  |  +-Sequence(CaseSensitive,'&lsquo;')
 *  +-Accept(217)
 *  |  +-Sequence(CaseSensitive,'&rsquo;')
 *  +-Accept(218)
 *  |  +-Sequence(CaseSensitive,'&sbquo;')
 *  +-Accept(219)
 *  |  +-Sequence(CaseSensitive,'&ldquo;')
 *  +-Accept(220)
 *  |  +-Sequence(CaseSensitive,'&rdquo;')
 *  +-Accept(221)
 *  |  +-Sequence(CaseSensitive,'&bdquo;')
 *  +-Accept(222)
 *  |  +-Sequence(CaseSensitive,'&dagger;')
 *  +-Accept(223)
 *  |  +-Sequence(CaseSensitive,'&Dagger;')
 *  +-Accept(224)
 *  |  +-Sequence(CaseSensitive,'&bull;')
 *  +-Accept(225)
 *  |  +-Sequence(CaseSensitive,'&hellip;')
 *  +-Accept(226)
 *  |  +-Sequence(CaseSensitive,'&permil;')
 *  +-Accept(227)
 *  |  +-Sequence(CaseSensitive,'&prime;')
 *  +-Accept(228)
 *  |  +-Sequence(CaseSensitive,'&Prime;')
 *  +-Accept(229)
 *  |  +-Sequence(CaseSensitive,'&lsaquo;')
 *  +-Accept(230)
 *  |  +-Sequence(CaseSensitive,'&rsaquo;')
 *  +-Accept(231)
 *  |  +-Sequence(CaseSensitive,'&oline;')
 *  +-Accept(232)
 *  |  +-Sequence(CaseSensitive,'&euro;')
 *  +-Accept(233)
 *  |  +-Sequence(CaseSensitive,'&trade;')
 *  +-Accept(234)
 *  |  +-Sequence(CaseSensitive,'&larr;')
 *  +-Accept(235)
 *  |  +-Sequence(CaseSensitive,'&uarr;')
 *  +-Accept(236)
 *  |  +-Sequence(CaseSensitive,'&rarr;')
 *  +-Accept(237)
 *  |  +-Sequence(CaseSensitive,'&darr;')
 *  +-Accept(238)
 *  |  +-Sequence(CaseSensitive,'&harr;')
 *  +-Accept(239)
 *  |  +-Sequence(CaseSensitive,'&crarr;')
 *  +-Accept(240)
 *  |  +-Sequence(CaseSensitive,'&lceil;')
 *  +-Accept(241)
 *  |  +-Sequence(CaseSensitive,'&rceil;')
 *  +-Accept(242)
 *  |  +-Sequence(CaseSensitive,'&lfloor;')
 *  +-Accept(243)
 *  |  +-Sequence(CaseSensitive,'&rfloor;')
 *  +-Accept(244)
 *  |  +-Sequence(CaseSensitive,'&loz;')
 *  +-Accept(245)
 *  |  +-Sequence(CaseSensitive,'&spades;')
 *  +-Accept(246)
 *  |  +-Sequence(CaseSensitive,'&clubs;')
 *  +-Accept(247)
 *  |  +-Sequence(CaseSensitive,'&hearts;')
 *  +-Accept(248)
 *  |  +-Sequence(CaseSensitive,'&diams;')
 *  +-Accept(249)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'%'] -> 1
 * '&' -> 2
 * ['''-';'] -> 1
 * '<' -> 3
 * ['='-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept0;
var current = *pNext++;
if(current < 0x27) /* ('&') ''' */  {
    if(current < 0x26) /* ('%') '&' */ 
        goto state0_1;
    goto state0_2;
}
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1;
if(current < 0x3D) /* ('<') '=' */ 
    goto state0_3;
goto state0_1;
/*
 * DFA STATE 1 (accepts to 249)
 */
state0_1:
pEnd = pNext;
goto accept0_249;
/*
 * DFA STATE 2 (accepts to 249)
 * '#' -> 4
 * 'A' -> 5
 * 'B' -> 6
 * 'C' -> 7
 * 'D' -> 8
 * 'E' -> 9
 * 'G' -> 10
 * 'I' -> 11
 * 'K' -> 12
 * 'L' -> 13
 * 'M' -> 14
 * 'N' -> 15
 * 'O' -> 16
 * 'P' -> 17
 * 'R' -> 18
 * 'S' -> 19
 * 'T' -> 20
 * 'U' -> 21
 * 'X' -> 22
 * 'Y' -> 23
 * 'Z' -> 24
 * 'a' -> 25
 * 'b' -> 26
 * 'c' -> 27
 * 'd' -> 28
 * 'e' -> 29
 * 'f' -> 30
 * 'g' -> 31
 * 'h' -> 32
 * 'i' -> 33
 * 'k' -> 34
 * 'l' -> 35
 * 'm' -> 36
 * 'n' -> 37
 * 'o' -> 38
 * 'p' -> 39
 * 'q' -> 40
 * 'r' -> 41
 * 's' -> 42
 * 't' -> 43
 * 'u' -> 44
 * 'x' -> 45
 * 'y' -> 46
 * 'z' -> 47
 */
state0_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x5A) /* ('Y') 'Z' */  {
    if(current < 0x4B) /* ('J') 'K' */  {
        if(current < 0x44) /* ('C') 'D' */  {
            if(current < 0x41) /* ('@') 'A' */  {
                if(current < 0x23) /* ('"') '#' */ 
                    goto accept0_249;
                if(current < 0x24) /* ('#') '$' */ 
                    goto state0_4;
                goto accept0_249;
            }
            if(current < 0x42) /* ('A') 'B' */ 
                goto state0_5;
            if(current < 0x43) /* ('B') 'C' */ 
                goto state0_6;
            goto state0_7;
        }
        if(current < 0x47) /* ('F') 'G' */  {
            if(current < 0x45) /* ('D') 'E' */ 
                goto state0_8;
            if(current < 0x46) /* ('E') 'F' */ 
                goto state0_9;
            goto accept0_249;
        }
        if(current < 0x49) /* ('H') 'I' */  {
            if(current < 0x48) /* ('G') 'H' */ 
                goto state0_10;
            goto accept0_249;
        }
        if(current < 0x4A) /* ('I') 'J' */ 
            goto state0_11;
        goto accept0_249;
    }
    if(current < 0x52) /* ('Q') 'R' */  {
        if(current < 0x4E) /* ('M') 'N' */  {
            if(current < 0x4C) /* ('K') 'L' */ 
                goto state0_12;
            if(current < 0x4D) /* ('L') 'M' */ 
                goto state0_13;
            goto state0_14;
        }
        if(current < 0x50) /* ('O') 'P' */  {
            if(current < 0x4F) /* ('N') 'O' */ 
                goto state0_15;
            goto state0_16;
        }
        if(current < 0x51) /* ('P') 'Q' */ 
            goto state0_17;
        goto accept0_249;
    }
    if(current < 0x55) /* ('T') 'U' */  {
        if(current < 0x53) /* ('R') 'S' */ 
            goto state0_18;
        if(current < 0x54) /* ('S') 'T' */ 
            goto state0_19;
        goto state0_20;
    }
    if(current < 0x58) /* ('W') 'X' */  {
        if(current < 0x56) /* ('U') 'V' */ 
            goto state0_21;
        goto accept0_249;
    }
    if(current < 0x59) /* ('X') 'Y' */ 
        goto state0_22;
    goto state0_23;
}
if(current < 0x6D) /* ('l') 'm' */  {
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x5B) /* ('Z') '[' */ 
                goto state0_24;
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_25;
        }
        if(current < 0x64) /* ('c') 'd' */  {
            if(current < 0x63) /* ('b') 'c' */ 
                goto state0_26;
            goto state0_27;
        }
        if(current < 0x65) /* ('d') 'e' */ 
            goto state0_28;
        goto state0_29;
    }
    if(current < 0x69) /* ('h') 'i' */  {
        if(current < 0x67) /* ('f') 'g' */ 
            goto state0_30;
        if(current < 0x68) /* ('g') 'h' */ 
            goto state0_31;
        goto state0_32;
    }
    if(current < 0x6B) /* ('j') 'k' */  {
        if(current < 0x6A) /* ('i') 'j' */ 
            goto state0_33;
        goto accept0_249;
    }
    if(current < 0x6C) /* ('k') 'l' */ 
        goto state0_34;
    goto state0_35;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x70) /* ('o') 'p' */  {
        if(current < 0x6E) /* ('m') 'n' */ 
            goto state0_36;
        if(current < 0x6F) /* ('n') 'o' */ 
            goto state0_37;
        goto state0_38;
    }
    if(current < 0x72) /* ('q') 'r' */  {
        if(current < 0x71) /* ('p') 'q' */ 
            goto state0_39;
        goto state0_40;
    }
    if(current < 0x73) /* ('r') 's' */ 
        goto state0_41;
    goto state0_42;
}
if(current < 0x78) /* ('w') 'x' */  {
    if(current < 0x75) /* ('t') 'u' */ 
        goto state0_43;
    if(current < 0x76) /* ('u') 'v' */ 
        goto state0_44;
    goto accept0_249;
}
if(current < 0x7A) /* ('y') 'z' */  {
    if(current < 0x79) /* ('x') 'y' */ 
        goto state0_45;
    goto state0_46;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_47;
goto accept0_249;
/*
 * DFA STATE 3 (accepts to 249)
 * '!' -> 48
 * '-' -> 49
 * '/' -> 50
 * '?' -> 51
 * ['A'-'R'] -> 52
 * 'S' -> 53
 * ['T'-'Z'] -> 52
 * ['a'-'r'] -> 52
 * 's' -> 53
 * ['t'-'z'] -> 52
 */
state0_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x22) /* ('!') '"' */  {
            if(current < 0x21) /* (' ') '!' */ 
                goto accept0_249;
            goto state0_48;
        }
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_249;
        goto state0_49;
    }
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2F) /* ('.') '/' */ 
            goto accept0_249;
        goto state0_50;
    }
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept0_249;
    goto state0_51;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x53) /* ('R') 'S' */  {
        if(current < 0x41) /* ('@') 'A' */ 
            goto accept0_249;
        goto state0_52;
    }
    if(current < 0x54) /* ('S') 'T' */ 
        goto state0_53;
    goto state0_52;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_52;
}
if(current < 0x74) /* ('s') 't' */ 
    goto state0_53;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_52;
goto accept0_249;
/*
 * DFA STATE 4
 * ['0'-'9'] -> 54
 * 'x' -> 55
 */
state0_4:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_249;
    goto state0_54;
}
if(current < 0x78) /* ('w') 'x' */ 
    goto accept0_249;
if(current < 0x79) /* ('x') 'y' */ 
    goto state0_55;
goto accept0_249;
/*
 * DFA STATE 5
 * 'E' -> 56
 * 'a' -> 57
 * 'c' -> 58
 * 'g' -> 59
 * 'l' -> 60
 * 'r' -> 61
 * 't' -> 62
 * 'u' -> 63
 */
state0_5:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x62) /* ('a') 'b' */  {
        if(current < 0x46) /* ('E') 'F' */  {
            if(current < 0x45) /* ('D') 'E' */ 
                goto accept0_249;
            goto state0_56;
        }
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        goto state0_57;
    }
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_58;
    }
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    goto state0_59;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x6D) /* ('l') 'm' */  {
        if(current < 0x6C) /* ('k') 'l' */ 
            goto accept0_249;
        goto state0_60;
    }
    if(current < 0x72) /* ('q') 'r' */ 
        goto accept0_249;
    goto state0_61;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto accept0_249;
    goto state0_62;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_63;
goto accept0_249;
/*
 * DFA STATE 6
 * 'e' -> 64
 */
state0_6:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_64;
goto accept0_249;
/*
 * DFA STATE 7
 * 'c' -> 65
 * 'h' -> 66
 */
state0_7:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */  {
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    goto state0_65;
}
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_66;
goto accept0_249;
/*
 * DFA STATE 8
 * 'a' -> 67
 * 'e' -> 68
 */
state0_8:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_67;
}
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_68;
goto accept0_249;
/*
 * DFA STATE 9
 * 'T' -> 69
 * 'a' -> 70
 * 'c' -> 71
 * 'g' -> 72
 * 'p' -> 73
 * 't' -> 74
 * 'u' -> 75
 */
state0_9:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x61) /* ('`') 'a' */  {
        if(current < 0x54) /* ('S') 'T' */ 
            goto accept0_249;
        if(current < 0x55) /* ('T') 'U' */ 
            goto state0_69;
        goto accept0_249;
    }
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_70;
        goto accept0_249;
    }
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_71;
    goto accept0_249;
}
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_72;
    if(current < 0x70) /* ('o') 'p' */ 
        goto accept0_249;
    goto state0_73;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto accept0_249;
    goto state0_74;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_75;
goto accept0_249;
/*
 * DFA STATE 10
 * 'a' -> 76
 */
state0_10:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_76;
goto accept0_249;
/*
 * DFA STATE 11
 * 'a' -> 77
 * 'c' -> 78
 * 'g' -> 79
 * 'o' -> 80
 * 'u' -> 81
 */
state0_11:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x62) /* ('a') 'b' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        goto state0_77;
    }
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_78;
    goto accept0_249;
}
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_79;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto accept0_249;
    goto state0_80;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_81;
goto accept0_249;
/*
 * DFA STATE 12
 * 'a' -> 82
 */
state0_12:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_82;
goto accept0_249;
/*
 * DFA STATE 13
 * 'a' -> 83
 */
state0_13:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_83;
goto accept0_249;
/*
 * DFA STATE 14
 * 'u' -> 84
 */
state0_14:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_84;
goto accept0_249;
/*
 * DFA STATE 15
 * 't' -> 85
 * 'u' -> 86
 */
state0_15:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto accept0_249;
    goto state0_85;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_86;
goto accept0_249;
/*
 * DFA STATE 16
 * 'E' -> 87
 * 'a' -> 88
 * 'c' -> 89
 * 'g' -> 90
 * 'm' -> 91
 * 's' -> 92
 * 't' -> 93
 * 'u' -> 94
 */
state0_16:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x61) /* ('`') 'a' */  {
        if(current < 0x45) /* ('D') 'E' */ 
            goto accept0_249;
        if(current < 0x46) /* ('E') 'F' */ 
            goto state0_87;
        goto accept0_249;
    }
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_88;
        goto accept0_249;
    }
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_89;
    goto accept0_249;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x6D) /* ('l') 'm' */  {
        if(current < 0x68) /* ('g') 'h' */ 
            goto state0_90;
        goto accept0_249;
    }
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state0_91;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto state0_92;
    goto state0_93;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_94;
goto accept0_249;
/*
 * DFA STATE 17
 * 'h' -> 95
 * 'i' -> 96
 * 'r' -> 97
 * 's' -> 98
 */
state0_17:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    if(current < 0x69) /* ('h') 'i' */ 
        goto state0_95;
    goto state0_96;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto accept0_249;
    goto state0_97;
}
if(current < 0x74) /* ('s') 't' */ 
    goto state0_98;
goto accept0_249;
/*
 * DFA STATE 18
 * 'h' -> 99
 */
state0_18:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_99;
goto accept0_249;
/*
 * DFA STATE 19
 * 'c' -> 100
 * 'i' -> 101
 */
state0_19:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */  {
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    goto state0_100;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_101;
goto accept0_249;
/*
 * DFA STATE 20
 * 'H' -> 102
 * 'a' -> 103
 * 'h' -> 104
 */
state0_20:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x48) /* ('G') 'H' */ 
        goto accept0_249;
    if(current < 0x49) /* ('H') 'I' */ 
        goto state0_102;
    goto accept0_249;
}
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x62) /* ('a') 'b' */ 
        goto state0_103;
    goto accept0_249;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_104;
goto accept0_249;
/*
 * DFA STATE 21
 * 'a' -> 105
 * 'c' -> 106
 * 'g' -> 107
 * 'p' -> 108
 * 'u' -> 109
 */
state0_21:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x62) /* ('a') 'b' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        goto state0_105;
    }
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_106;
    goto accept0_249;
}
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_107;
    if(current < 0x70) /* ('o') 'p' */ 
        goto accept0_249;
    goto state0_108;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_109;
goto accept0_249;
/*
 * DFA STATE 22
 * 'i' -> 110
 */
state0_22:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_110;
goto accept0_249;
/*
 * DFA STATE 23
 * 'a' -> 111
 * 'u' -> 112
 */
state0_23:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_111;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_112;
goto accept0_249;
/*
 * DFA STATE 24
 * 'e' -> 113
 */
state0_24:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_113;
goto accept0_249;
/*
 * DFA STATE 25
 * 'a' -> 114
 * 'c' -> 115
 * 'e' -> 116
 * 'g' -> 117
 * 'l' -> 118
 * 'm' -> 119
 * 'n' -> 120
 * 'r' -> 121
 * 's' -> 122
 * 't' -> 123
 * 'u' -> 124
 */
state0_25:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_114;
        }
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_115;
    }
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x65) /* ('d') 'e' */ 
            goto accept0_249;
        goto state0_116;
    }
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_117;
    goto accept0_249;
}
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */  {
        if(current < 0x6D) /* ('l') 'm' */ 
            goto state0_118;
        goto state0_119;
    }
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state0_120;
    goto accept0_249;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state0_121;
    goto state0_122;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_123;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_124;
goto accept0_249;
/*
 * DFA STATE 26
 * 'd' -> 125
 * 'e' -> 126
 * 'r' -> 127
 * 'u' -> 128
 */
state0_26:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x65) /* ('d') 'e' */  {
        if(current < 0x64) /* ('c') 'd' */ 
            goto accept0_249;
        goto state0_125;
    }
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_126;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state0_127;
    goto accept0_249;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_128;
goto accept0_249;
/*
 * DFA STATE 27
 * 'a' -> 129
 * 'c' -> 130
 * 'e' -> 131
 * 'h' -> 132
 * 'i' -> 133
 * 'l' -> 134
 * 'o' -> 135
 * 'r' -> 136
 * 'u' -> 137
 */
state0_27:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_129;
        }
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_130;
    }
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x65) /* ('d') 'e' */ 
            goto accept0_249;
        goto state0_131;
    }
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    if(current < 0x69) /* ('h') 'i' */ 
        goto state0_132;
    goto state0_133;
}
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x6D) /* ('l') 'm' */  {
        if(current < 0x6C) /* ('k') 'l' */ 
            goto accept0_249;
        goto state0_134;
    }
    if(current < 0x6F) /* ('n') 'o' */ 
        goto accept0_249;
    goto state0_135;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto accept0_249;
    goto state0_136;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_137;
goto accept0_249;
/*
 * DFA STATE 28
 * 'a' -> 138
 * 'e' -> 139
 * 'i' -> 140
 */
state0_28:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    if(current < 0x62) /* ('a') 'b' */ 
        goto state0_138;
    goto accept0_249;
}
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_139;
    goto accept0_249;
}
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_140;
goto accept0_249;
/*
 * DFA STATE 29
 * 'a' -> 141
 * 'c' -> 142
 * 'g' -> 143
 * 'm' -> 144
 * 'n' -> 145
 * 'p' -> 146
 * 'q' -> 147
 * 't' -> 148
 * 'u' -> 149
 * 'x' -> 150
 */
state0_29:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_141;
        }
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_142;
    }
    if(current < 0x68) /* ('g') 'h' */  {
        if(current < 0x67) /* ('f') 'g' */ 
            goto accept0_249;
        goto state0_143;
    }
    if(current < 0x6D) /* ('l') 'm' */ 
        goto accept0_249;
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state0_144;
    goto state0_145;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x71) /* ('p') 'q' */  {
        if(current < 0x70) /* ('o') 'p' */ 
            goto accept0_249;
        goto state0_146;
    }
    if(current < 0x72) /* ('q') 'r' */ 
        goto state0_147;
    goto accept0_249;
}
if(current < 0x76) /* ('u') 'v' */  {
    if(current < 0x75) /* ('t') 'u' */ 
        goto state0_148;
    goto state0_149;
}
if(current < 0x78) /* ('w') 'x' */ 
    goto accept0_249;
if(current < 0x79) /* ('x') 'y' */ 
    goto state0_150;
goto accept0_249;
/*
 * DFA STATE 30
 * 'n' -> 151
 * 'o' -> 152
 * 'r' -> 153
 */
state0_30:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto accept0_249;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state0_151;
    goto state0_152;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_153;
goto accept0_249;
/*
 * DFA STATE 31
 * 'a' -> 154
 * 'e' -> 155
 * 't' -> 156
 */
state0_31:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    if(current < 0x62) /* ('a') 'b' */ 
        goto state0_154;
    goto accept0_249;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_155;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_156;
goto accept0_249;
/*
 * DFA STATE 32
 * 'a' -> 157
 * 'e' -> 158
 */
state0_32:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_157;
}
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_158;
goto accept0_249;
/*
 * DFA STATE 33
 * 'a' -> 159
 * 'c' -> 160
 * 'e' -> 161
 * 'g' -> 162
 * 'n' -> 163
 * 'o' -> 164
 * 'q' -> 165
 * 's' -> 166
 * 'u' -> 167
 */
state0_33:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_159;
        }
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_160;
    }
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x65) /* ('d') 'e' */ 
            goto accept0_249;
        goto state0_161;
    }
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_162;
    goto accept0_249;
}
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x70) /* ('o') 'p' */  {
        if(current < 0x6F) /* ('n') 'o' */ 
            goto state0_163;
        goto state0_164;
    }
    if(current < 0x71) /* ('p') 'q' */ 
        goto accept0_249;
    goto state0_165;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto accept0_249;
    goto state0_166;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_167;
goto accept0_249;
/*
 * DFA STATE 34
 * 'a' -> 168
 */
state0_34:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_168;
goto accept0_249;
/*
 * DFA STATE 35
 * 'a' -> 169
 * 'c' -> 170
 * 'd' -> 171
 * 'e' -> 172
 * 'f' -> 173
 * 'o' -> 174
 * 'r' -> 175
 * 's' -> 176
 * 't' -> 177
 */
state0_35:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_169;
        goto accept0_249;
    }
    if(current < 0x65) /* ('d') 'e' */  {
        if(current < 0x64) /* ('c') 'd' */ 
            goto state0_170;
        goto state0_171;
    }
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_172;
    goto state0_173;
}
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6F) /* ('n') 'o' */ 
        goto accept0_249;
    if(current < 0x70) /* ('o') 'p' */ 
        goto state0_174;
    goto accept0_249;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state0_175;
    goto state0_176;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_177;
goto accept0_249;
/*
 * DFA STATE 36
 * 'a' -> 178
 * 'd' -> 179
 * 'i' -> 180
 * 'u' -> 181
 */
state0_36:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x62) /* ('a') 'b' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        goto state0_178;
    }
    if(current < 0x64) /* ('c') 'd' */ 
        goto accept0_249;
    goto state0_179;
}
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x69) /* ('h') 'i' */ 
        goto accept0_249;
    goto state0_180;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_181;
goto accept0_249;
/*
 * DFA STATE 37
 * 'a' -> 182
 * 'b' -> 183
 * 'd' -> 184
 * 'e' -> 185
 * 'i' -> 186
 * 'o' -> 187
 * 's' -> 188
 * 't' -> 189
 * 'u' -> 190
 */
state0_37:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_182;
        goto state0_183;
    }
    if(current < 0x65) /* ('d') 'e' */  {
        if(current < 0x64) /* ('c') 'd' */ 
            goto accept0_249;
        goto state0_184;
    }
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_185;
    goto accept0_249;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x6F) /* ('n') 'o' */  {
        if(current < 0x6A) /* ('i') 'j' */ 
            goto state0_186;
        goto accept0_249;
    }
    if(current < 0x70) /* ('o') 'p' */ 
        goto state0_187;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto state0_188;
    goto state0_189;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_190;
goto accept0_249;
/*
 * DFA STATE 38
 * 'a' -> 191
 * 'c' -> 192
 * 'e' -> 193
 * 'g' -> 194
 * 'l' -> 195
 * 'm' -> 196
 * 'p' -> 197
 * 'r' -> 198
 * 's' -> 199
 * 't' -> 200
 * 'u' -> 201
 */
state0_38:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */  {
            if(current < 0x61) /* ('`') 'a' */ 
                goto accept0_249;
            goto state0_191;
        }
        if(current < 0x63) /* ('b') 'c' */ 
            goto accept0_249;
        goto state0_192;
    }
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x65) /* ('d') 'e' */ 
            goto accept0_249;
        goto state0_193;
    }
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    if(current < 0x68) /* ('g') 'h' */ 
        goto state0_194;
    goto accept0_249;
}
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */  {
        if(current < 0x6D) /* ('l') 'm' */ 
            goto state0_195;
        goto state0_196;
    }
    if(current < 0x70) /* ('o') 'p' */ 
        goto accept0_249;
    if(current < 0x71) /* ('p') 'q' */ 
        goto state0_197;
    goto accept0_249;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state0_198;
    goto state0_199;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_200;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_201;
goto accept0_249;
/*
 * DFA STATE 39
 * 'a' -> 202
 * 'e' -> 203
 * 'h' -> 204
 * 'i' -> 205
 * 'l' -> 206
 * 'o' -> 207
 * 'r' -> 208
 * 's' -> 209
 */
state0_39:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x65) /* ('d') 'e' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_202;
        goto accept0_249;
    }
    if(current < 0x68) /* ('g') 'h' */  {
        if(current < 0x66) /* ('e') 'f' */ 
            goto state0_203;
        goto accept0_249;
    }
    if(current < 0x69) /* ('h') 'i' */ 
        goto state0_204;
    goto state0_205;
}
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x6D) /* ('l') 'm' */  {
        if(current < 0x6C) /* ('k') 'l' */ 
            goto accept0_249;
        goto state0_206;
    }
    if(current < 0x6F) /* ('n') 'o' */ 
        goto accept0_249;
    goto state0_207;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto accept0_249;
    goto state0_208;
}
if(current < 0x74) /* ('s') 't' */ 
    goto state0_209;
goto accept0_249;
/*
 * DFA STATE 40
 * 'u' -> 210
 */
state0_40:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_210;
goto accept0_249;
/*
 * DFA STATE 41
 * 'a' -> 211
 * 'c' -> 212
 * 'd' -> 213
 * 'e' -> 214
 * 'f' -> 215
 * 'h' -> 216
 * 'l' -> 217
 * 's' -> 218
 */
state0_41:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_211;
        goto accept0_249;
    }
    if(current < 0x65) /* ('d') 'e' */  {
        if(current < 0x64) /* ('c') 'd' */ 
            goto state0_212;
        goto state0_213;
    }
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_214;
    goto state0_215;
}
if(current < 0x6C) /* ('k') 'l' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    if(current < 0x69) /* ('h') 'i' */ 
        goto state0_216;
    goto accept0_249;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x6D) /* ('l') 'm' */ 
        goto state0_217;
    goto accept0_249;
}
if(current < 0x74) /* ('s') 't' */ 
    goto state0_218;
goto accept0_249;
/*
 * DFA STATE 42
 * 'b' -> 219
 * 'c' -> 220
 * 'd' -> 221
 * 'e' -> 222
 * 'h' -> 223
 * 'i' -> 224
 * 'p' -> 225
 * 'u' -> 226
 * 'z' -> 227
 */
state0_42:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x64) /* ('c') 'd' */  {
        if(current < 0x62) /* ('a') 'b' */ 
            goto accept0_249;
        if(current < 0x63) /* ('b') 'c' */ 
            goto state0_219;
        goto state0_220;
    }
    if(current < 0x66) /* ('e') 'f' */  {
        if(current < 0x65) /* ('d') 'e' */ 
            goto state0_221;
        goto state0_222;
    }
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    goto state0_223;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x70) /* ('o') 'p' */  {
        if(current < 0x6A) /* ('i') 'j' */ 
            goto state0_224;
        goto accept0_249;
    }
    if(current < 0x71) /* ('p') 'q' */ 
        goto state0_225;
    goto accept0_249;
}
if(current < 0x7A) /* ('y') 'z' */  {
    if(current < 0x76) /* ('u') 'v' */ 
        goto state0_226;
    goto accept0_249;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_227;
goto accept0_249;
/*
 * DFA STATE 43
 * 'a' -> 228
 * 'h' -> 229
 * 'i' -> 230
 * 'r' -> 231
 */
state0_43:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x62) /* ('a') 'b' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        goto state0_228;
    }
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    goto state0_229;
}
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6A) /* ('i') 'j' */ 
        goto state0_230;
    goto accept0_249;
}
if(current < 0x73) /* ('r') 's' */ 
    goto state0_231;
goto accept0_249;
/*
 * DFA STATE 44
 * 'a' -> 232
 * 'c' -> 233
 * 'g' -> 234
 * 'm' -> 235
 * 'p' -> 236
 * 'u' -> 237
 */
state0_44:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x63) /* ('b') 'c' */  {
        if(current < 0x61) /* ('`') 'a' */ 
            goto accept0_249;
        if(current < 0x62) /* ('a') 'b' */ 
            goto state0_232;
        goto accept0_249;
    }
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_233;
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    goto state0_234;
}
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x6D) /* ('l') 'm' */ 
        goto accept0_249;
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state0_235;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x71) /* ('p') 'q' */ 
        goto state0_236;
    goto accept0_249;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_237;
goto accept0_249;
/*
 * DFA STATE 45
 * 'i' -> 238
 */
state0_45:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_238;
goto accept0_249;
/*
 * DFA STATE 46
 * 'a' -> 239
 * 'e' -> 240
 * 'u' -> 241
 */
state0_46:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    if(current < 0x62) /* ('a') 'b' */ 
        goto state0_239;
    goto accept0_249;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_240;
    goto accept0_249;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_241;
goto accept0_249;
/*
 * DFA STATE 47
 * 'e' -> 242
 * 'w' -> 243
 */
state0_47:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x66) /* ('e') 'f' */  {
    if(current < 0x65) /* ('d') 'e' */ 
        goto accept0_249;
    goto state0_242;
}
if(current < 0x77) /* ('v') 'w' */ 
    goto accept0_249;
if(current < 0x78) /* ('w') 'x' */ 
    goto state0_243;
goto accept0_249;
/*
 * DFA STATE 48 (accepts to 3)
 * '[' -> 244
 */
state0_48:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept0_3;
if(current < 0x5C) /* ('[') '\' */ 
    goto state0_244;
goto accept0_3;
/*
 * DFA STATE 49
 * '-' -> 245
 */
state0_49:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept0_249;
if(current < 0x2E) /* ('-') '.' */ 
    goto state0_245;
goto accept0_249;
/*
 * DFA STATE 50
 * ['A'-'Z'] -> 246
 * ['a'-'z'] -> 246
 */
state0_50:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_249;
    goto state0_246;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_246;
goto accept0_249;
/*
 * DFA STATE 51 (accepts to 1)
 */
state0_51:
pEnd = pNext;
goto accept0_1;
/*
 * DFA STATE 52 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'Z'] -> 247
 * ['a'-'z'] -> 247
 */
state0_52:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        goto state0_247;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_6;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 53 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'B'] -> 247
 * 'C' -> 248
 * ['D'-'Z'] -> 247
 * ['a'-'b'] -> 247
 * 'c' -> 248
 * ['d'-'z'] -> 247
 */
state0_53:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x43) /* ('B') 'C' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_247;
        goto accept0_6;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto state0_247;
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x44) /* ('C') 'D' */ 
        goto state0_248;
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_247;
    goto accept0_6;
}
if(current < 0x64) /* ('c') 'd' */  {
    if(current < 0x63) /* ('b') 'c' */ 
        goto state0_247;
    goto state0_248;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 54
 * ['0'-'9'] -> 54
 * ';' -> 249
 */
state0_54:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_249;
    goto state0_54;
}
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_249;
goto accept0_249;
/*
 * DFA STATE 55
 * ['0'-'9'] -> 250
 * ['A'-'F'] -> 250
 * ['a'-'f'] -> 250
 */
state0_55:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_249;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state0_250;
    goto accept0_249;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x47) /* ('F') 'G' */ 
        goto state0_250;
    goto accept0_249;
}
if(current < 0x67) /* ('f') 'g' */ 
    goto state0_250;
goto accept0_249;
/*
 * DFA STATE 56
 * 'l' -> 251
 */
state0_56:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_251;
goto accept0_249;
/*
 * DFA STATE 57
 * 'c' -> 252
 */
state0_57:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_252;
goto accept0_249;
/*
 * DFA STATE 58
 * 'i' -> 253
 */
state0_58:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_253;
goto accept0_249;
/*
 * DFA STATE 59
 * 'r' -> 254
 */
state0_59:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_254;
goto accept0_249;
/*
 * DFA STATE 60
 * 'p' -> 255
 */
state0_60:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_255;
goto accept0_249;
/*
 * DFA STATE 61
 * 'i' -> 256
 */
state0_61:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_256;
goto accept0_249;
/*
 * DFA STATE 62
 * 'i' -> 257
 */
state0_62:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_257;
goto accept0_249;
/*
 * DFA STATE 63
 * 'm' -> 258
 */
state0_63:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_258;
goto accept0_249;
/*
 * DFA STATE 64
 * 't' -> 259
 */
state0_64:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_259;
goto accept0_249;
/*
 * DFA STATE 65
 * 'e' -> 260
 */
state0_65:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_260;
goto accept0_249;
/*
 * DFA STATE 66
 * 'i' -> 261
 */
state0_66:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_261;
goto accept0_249;
/*
 * DFA STATE 67
 * 'g' -> 262
 */
state0_67:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_262;
goto accept0_249;
/*
 * DFA STATE 68
 * 'l' -> 263
 */
state0_68:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_263;
goto accept0_249;
/*
 * DFA STATE 69
 * 'H' -> 264
 */
state0_69:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x48) /* ('G') 'H' */ 
    goto accept0_249;
if(current < 0x49) /* ('H') 'I' */ 
    goto state0_264;
goto accept0_249;
/*
 * DFA STATE 70
 * 'c' -> 265
 */
state0_70:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_265;
goto accept0_249;
/*
 * DFA STATE 71
 * 'i' -> 266
 */
state0_71:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_266;
goto accept0_249;
/*
 * DFA STATE 72
 * 'r' -> 267
 */
state0_72:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_267;
goto accept0_249;
/*
 * DFA STATE 73
 * 's' -> 268
 */
state0_73:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_268;
goto accept0_249;
/*
 * DFA STATE 74
 * 'a' -> 269
 */
state0_74:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_269;
goto accept0_249;
/*
 * DFA STATE 75
 * 'm' -> 270
 */
state0_75:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_270;
goto accept0_249;
/*
 * DFA STATE 76
 * 'm' -> 271
 */
state0_76:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_271;
goto accept0_249;
/*
 * DFA STATE 77
 * 'c' -> 272
 */
state0_77:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_272;
goto accept0_249;
/*
 * DFA STATE 78
 * 'i' -> 273
 */
state0_78:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_273;
goto accept0_249;
/*
 * DFA STATE 79
 * 'r' -> 274
 */
state0_79:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_274;
goto accept0_249;
/*
 * DFA STATE 80
 * 't' -> 275
 */
state0_80:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_275;
goto accept0_249;
/*
 * DFA STATE 81
 * 'm' -> 276
 */
state0_81:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_276;
goto accept0_249;
/*
 * DFA STATE 82
 * 'p' -> 277
 */
state0_82:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_277;
goto accept0_249;
/*
 * DFA STATE 83
 * 'm' -> 278
 */
state0_83:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_278;
goto accept0_249;
/*
 * DFA STATE 84
 * ';' -> 279
 */
state0_84:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_279;
goto accept0_249;
/*
 * DFA STATE 85
 * 'i' -> 280
 */
state0_85:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_280;
goto accept0_249;
/*
 * DFA STATE 86
 * ';' -> 281
 */
state0_86:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_281;
goto accept0_249;
/*
 * DFA STATE 87
 * 'l' -> 282
 */
state0_87:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_282;
goto accept0_249;
/*
 * DFA STATE 88
 * 'c' -> 283
 */
state0_88:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_283;
goto accept0_249;
/*
 * DFA STATE 89
 * 'i' -> 284
 */
state0_89:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_284;
goto accept0_249;
/*
 * DFA STATE 90
 * 'r' -> 285
 */
state0_90:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_285;
goto accept0_249;
/*
 * DFA STATE 91
 * 'e' -> 286
 * 'i' -> 287
 */
state0_91:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x66) /* ('e') 'f' */  {
    if(current < 0x65) /* ('d') 'e' */ 
        goto accept0_249;
    goto state0_286;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_287;
goto accept0_249;
/*
 * DFA STATE 92
 * 'l' -> 288
 */
state0_92:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_288;
goto accept0_249;
/*
 * DFA STATE 93
 * 'i' -> 289
 */
state0_93:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_289;
goto accept0_249;
/*
 * DFA STATE 94
 * 'm' -> 290
 */
state0_94:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_290;
goto accept0_249;
/*
 * DFA STATE 95
 * 'i' -> 291
 */
state0_95:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_291;
goto accept0_249;
/*
 * DFA STATE 96
 * ';' -> 292
 */
state0_96:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_292;
goto accept0_249;
/*
 * DFA STATE 97
 * 'i' -> 293
 */
state0_97:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_293;
goto accept0_249;
/*
 * DFA STATE 98
 * 'i' -> 294
 */
state0_98:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_294;
goto accept0_249;
/*
 * DFA STATE 99
 * 'o' -> 295
 */
state0_99:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_295;
goto accept0_249;
/*
 * DFA STATE 100
 * 'a' -> 296
 */
state0_100:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_296;
goto accept0_249;
/*
 * DFA STATE 101
 * 'g' -> 297
 */
state0_101:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_297;
goto accept0_249;
/*
 * DFA STATE 102
 * 'O' -> 298
 */
state0_102:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x4F) /* ('N') 'O' */ 
    goto accept0_249;
if(current < 0x50) /* ('O') 'P' */ 
    goto state0_298;
goto accept0_249;
/*
 * DFA STATE 103
 * 'u' -> 299
 */
state0_103:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_299;
goto accept0_249;
/*
 * DFA STATE 104
 * 'e' -> 300
 */
state0_104:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_300;
goto accept0_249;
/*
 * DFA STATE 105
 * 'c' -> 301
 */
state0_105:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_301;
goto accept0_249;
/*
 * DFA STATE 106
 * 'i' -> 302
 */
state0_106:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_302;
goto accept0_249;
/*
 * DFA STATE 107
 * 'r' -> 303
 */
state0_107:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_303;
goto accept0_249;
/*
 * DFA STATE 108
 * 's' -> 304
 */
state0_108:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_304;
goto accept0_249;
/*
 * DFA STATE 109
 * 'm' -> 305
 */
state0_109:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_305;
goto accept0_249;
/*
 * DFA STATE 110
 * ';' -> 306
 */
state0_110:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_306;
goto accept0_249;
/*
 * DFA STATE 111
 * 'c' -> 307
 */
state0_111:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_307;
goto accept0_249;
/*
 * DFA STATE 112
 * 'm' -> 308
 */
state0_112:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_308;
goto accept0_249;
/*
 * DFA STATE 113
 * 't' -> 309
 */
state0_113:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_309;
goto accept0_249;
/*
 * DFA STATE 114
 * 'c' -> 310
 */
state0_114:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_310;
goto accept0_249;
/*
 * DFA STATE 115
 * 'i' -> 311
 * 'u' -> 312
 */
state0_115:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x69) /* ('h') 'i' */ 
        goto accept0_249;
    goto state0_311;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_312;
goto accept0_249;
/*
 * DFA STATE 116
 * 'l' -> 313
 */
state0_116:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_313;
goto accept0_249;
/*
 * DFA STATE 117
 * 'r' -> 314
 */
state0_117:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_314;
goto accept0_249;
/*
 * DFA STATE 118
 * 'p' -> 315
 */
state0_118:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_315;
goto accept0_249;
/*
 * DFA STATE 119
 * 'p' -> 316
 */
state0_119:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_316;
goto accept0_249;
/*
 * DFA STATE 120
 * 'd' -> 317
 * 'g' -> 318
 */
state0_120:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x64) /* ('c') 'd' */ 
        goto accept0_249;
    goto state0_317;
}
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_318;
goto accept0_249;
/*
 * DFA STATE 121
 * 'i' -> 319
 */
state0_121:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_319;
goto accept0_249;
/*
 * DFA STATE 122
 * 'y' -> 320
 */
state0_122:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x79) /* ('x') 'y' */ 
    goto accept0_249;
if(current < 0x7A) /* ('y') 'z' */ 
    goto state0_320;
goto accept0_249;
/*
 * DFA STATE 123
 * 'i' -> 321
 */
state0_123:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_321;
goto accept0_249;
/*
 * DFA STATE 124
 * 'm' -> 322
 */
state0_124:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_322;
goto accept0_249;
/*
 * DFA STATE 125
 * 'q' -> 323
 */
state0_125:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_323;
goto accept0_249;
/*
 * DFA STATE 126
 * 't' -> 324
 */
state0_126:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_324;
goto accept0_249;
/*
 * DFA STATE 127
 * 'v' -> 325
 */
state0_127:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_325;
goto accept0_249;
/*
 * DFA STATE 128
 * 'l' -> 326
 */
state0_128:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_326;
goto accept0_249;
/*
 * DFA STATE 129
 * 'p' -> 327
 */
state0_129:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_327;
goto accept0_249;
/*
 * DFA STATE 130
 * 'e' -> 328
 */
state0_130:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_328;
goto accept0_249;
/*
 * DFA STATE 131
 * 'd' -> 329
 * 'n' -> 330
 */
state0_131:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x64) /* ('c') 'd' */ 
        goto accept0_249;
    goto state0_329;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_330;
goto accept0_249;
/*
 * DFA STATE 132
 * 'i' -> 331
 */
state0_132:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_331;
goto accept0_249;
/*
 * DFA STATE 133
 * 'r' -> 332
 */
state0_133:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_332;
goto accept0_249;
/*
 * DFA STATE 134
 * 'u' -> 333
 */
state0_134:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_333;
goto accept0_249;
/*
 * DFA STATE 135
 * 'n' -> 334
 * 'p' -> 335
 */
state0_135:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto accept0_249;
    goto state0_334;
}
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_335;
goto accept0_249;
/*
 * DFA STATE 136
 * 'a' -> 336
 */
state0_136:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_336;
goto accept0_249;
/*
 * DFA STATE 137
 * 'p' -> 337
 * 'r' -> 338
 */
state0_137:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x70) /* ('o') 'p' */ 
        goto accept0_249;
    goto state0_337;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_338;
goto accept0_249;
/*
 * DFA STATE 138
 * 'g' -> 339
 * 'r' -> 340
 */
state0_138:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    goto state0_339;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_340;
goto accept0_249;
/*
 * DFA STATE 139
 * 'g' -> 341
 * 'l' -> 342
 */
state0_139:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    goto state0_341;
}
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_342;
goto accept0_249;
/*
 * DFA STATE 140
 * 'a' -> 343
 * 'v' -> 344
 */
state0_140:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_343;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_344;
goto accept0_249;
/*
 * DFA STATE 141
 * 'c' -> 345
 */
state0_141:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_345;
goto accept0_249;
/*
 * DFA STATE 142
 * 'i' -> 346
 */
state0_142:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_346;
goto accept0_249;
/*
 * DFA STATE 143
 * 'r' -> 347
 */
state0_143:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_347;
goto accept0_249;
/*
 * DFA STATE 144
 * 'p' -> 348
 * 's' -> 349
 */
state0_144:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x70) /* ('o') 'p' */ 
        goto accept0_249;
    goto state0_348;
}
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_349;
goto accept0_249;
/*
 * DFA STATE 145
 * 's' -> 350
 */
state0_145:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_350;
goto accept0_249;
/*
 * DFA STATE 146
 * 's' -> 351
 */
state0_146:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_351;
goto accept0_249;
/*
 * DFA STATE 147
 * 'u' -> 352
 */
state0_147:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_352;
goto accept0_249;
/*
 * DFA STATE 148
 * 'a' -> 353
 * 'h' -> 354
 */
state0_148:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_353;
}
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_354;
goto accept0_249;
/*
 * DFA STATE 149
 * 'm' -> 355
 * 'r' -> 356
 */
state0_149:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */  {
    if(current < 0x6D) /* ('l') 'm' */ 
        goto accept0_249;
    goto state0_355;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_356;
goto accept0_249;
/*
 * DFA STATE 150
 * 'i' -> 357
 */
state0_150:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_357;
goto accept0_249;
/*
 * DFA STATE 151
 * 'o' -> 358
 */
state0_151:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_358;
goto accept0_249;
/*
 * DFA STATE 152
 * 'r' -> 359
 */
state0_152:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_359;
goto accept0_249;
/*
 * DFA STATE 153
 * 'a' -> 360
 */
state0_153:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_360;
goto accept0_249;
/*
 * DFA STATE 154
 * 'm' -> 361
 */
state0_154:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_361;
goto accept0_249;
/*
 * DFA STATE 155
 * ';' -> 362
 */
state0_155:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_362;
goto accept0_249;
/*
 * DFA STATE 156
 * ';' -> 363
 */
state0_156:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_363;
goto accept0_249;
/*
 * DFA STATE 157
 * 'r' -> 364
 */
state0_157:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_364;
goto accept0_249;
/*
 * DFA STATE 158
 * 'a' -> 365
 * 'l' -> 366
 */
state0_158:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_365;
}
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_366;
goto accept0_249;
/*
 * DFA STATE 159
 * 'c' -> 367
 */
state0_159:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_367;
goto accept0_249;
/*
 * DFA STATE 160
 * 'i' -> 368
 */
state0_160:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_368;
goto accept0_249;
/*
 * DFA STATE 161
 * 'x' -> 369
 */
state0_161:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x78) /* ('w') 'x' */ 
    goto accept0_249;
if(current < 0x79) /* ('x') 'y' */ 
    goto state0_369;
goto accept0_249;
/*
 * DFA STATE 162
 * 'r' -> 370
 */
state0_162:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_370;
goto accept0_249;
/*
 * DFA STATE 163
 * 'f' -> 371
 * 't' -> 372
 */
state0_163:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto accept0_249;
    goto state0_371;
}
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_372;
goto accept0_249;
/*
 * DFA STATE 164
 * 't' -> 373
 */
state0_164:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_373;
goto accept0_249;
/*
 * DFA STATE 165
 * 'u' -> 374
 */
state0_165:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_374;
goto accept0_249;
/*
 * DFA STATE 166
 * 'i' -> 375
 */
state0_166:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_375;
goto accept0_249;
/*
 * DFA STATE 167
 * 'm' -> 376
 */
state0_167:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_376;
goto accept0_249;
/*
 * DFA STATE 168
 * 'p' -> 377
 */
state0_168:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_377;
goto accept0_249;
/*
 * DFA STATE 169
 * 'm' -> 378
 * 'q' -> 379
 * 'r' -> 380
 */
state0_169:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x6D) /* ('l') 'm' */ 
        goto accept0_249;
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state0_378;
    goto accept0_249;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_379;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_380;
goto accept0_249;
/*
 * DFA STATE 170
 * 'e' -> 381
 */
state0_170:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_381;
goto accept0_249;
/*
 * DFA STATE 171
 * 'q' -> 382
 */
state0_171:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_382;
goto accept0_249;
/*
 * DFA STATE 172
 * ';' -> 383
 */
state0_172:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_383;
goto accept0_249;
/*
 * DFA STATE 173
 * 'l' -> 384
 */
state0_173:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_384;
goto accept0_249;
/*
 * DFA STATE 174
 * 'w' -> 385
 * 'z' -> 386
 */
state0_174:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x78) /* ('w') 'x' */  {
    if(current < 0x77) /* ('v') 'w' */ 
        goto accept0_249;
    goto state0_385;
}
if(current < 0x7A) /* ('y') 'z' */ 
    goto accept0_249;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_386;
goto accept0_249;
/*
 * DFA STATE 175
 * 'm' -> 387
 */
state0_175:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_387;
goto accept0_249;
/*
 * DFA STATE 176
 * 'a' -> 388
 * 'q' -> 389
 */
state0_176:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_388;
}
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_389;
goto accept0_249;
/*
 * DFA STATE 177
 * ';' -> 390
 */
state0_177:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_390;
goto accept0_249;
/*
 * DFA STATE 178
 * 'c' -> 391
 */
state0_178:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_391;
goto accept0_249;
/*
 * DFA STATE 179
 * 'a' -> 392
 */
state0_179:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_392;
goto accept0_249;
/*
 * DFA STATE 180
 * 'c' -> 393
 * 'd' -> 394
 * 'n' -> 395
 */
state0_180:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    if(current < 0x64) /* ('c') 'd' */ 
        goto state0_393;
    goto state0_394;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_395;
goto accept0_249;
/*
 * DFA STATE 181
 * ';' -> 396
 */
state0_181:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_396;
goto accept0_249;
/*
 * DFA STATE 182
 * 'b' -> 397
 */
state0_182:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_397;
goto accept0_249;
/*
 * DFA STATE 183
 * 's' -> 398
 */
state0_183:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_398;
goto accept0_249;
/*
 * DFA STATE 184
 * 'a' -> 399
 */
state0_184:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_399;
goto accept0_249;
/*
 * DFA STATE 185
 * ';' -> 400
 */
state0_185:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_400;
goto accept0_249;
/*
 * DFA STATE 186
 * ';' -> 401
 */
state0_186:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_401;
goto accept0_249;
/*
 * DFA STATE 187
 * 't' -> 402
 */
state0_187:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_402;
goto accept0_249;
/*
 * DFA STATE 188
 * 'u' -> 403
 */
state0_188:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_403;
goto accept0_249;
/*
 * DFA STATE 189
 * 'i' -> 404
 */
state0_189:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_404;
goto accept0_249;
/*
 * DFA STATE 190
 * ';' -> 405
 */
state0_190:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_405;
goto accept0_249;
/*
 * DFA STATE 191
 * 'c' -> 406
 */
state0_191:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_406;
goto accept0_249;
/*
 * DFA STATE 192
 * 'i' -> 407
 */
state0_192:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_407;
goto accept0_249;
/*
 * DFA STATE 193
 * 'l' -> 408
 */
state0_193:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_408;
goto accept0_249;
/*
 * DFA STATE 194
 * 'r' -> 409
 */
state0_194:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_409;
goto accept0_249;
/*
 * DFA STATE 195
 * 'i' -> 410
 */
state0_195:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_410;
goto accept0_249;
/*
 * DFA STATE 196
 * 'e' -> 411
 * 'i' -> 412
 */
state0_196:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x66) /* ('e') 'f' */  {
    if(current < 0x65) /* ('d') 'e' */ 
        goto accept0_249;
    goto state0_411;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_412;
goto accept0_249;
/*
 * DFA STATE 197
 * 'l' -> 413
 */
state0_197:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_413;
goto accept0_249;
/*
 * DFA STATE 198
 * ';' -> 414
 * 'd' -> 415
 */
state0_198:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_414;
}
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_415;
goto accept0_249;
/*
 * DFA STATE 199
 * 'l' -> 416
 */
state0_199:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_416;
goto accept0_249;
/*
 * DFA STATE 200
 * 'i' -> 417
 */
state0_200:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_417;
goto accept0_249;
/*
 * DFA STATE 201
 * 'm' -> 418
 */
state0_201:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_418;
goto accept0_249;
/*
 * DFA STATE 202
 * 'r' -> 419
 */
state0_202:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_419;
goto accept0_249;
/*
 * DFA STATE 203
 * 'r' -> 420
 */
state0_203:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_420;
goto accept0_249;
/*
 * DFA STATE 204
 * 'i' -> 421
 */
state0_204:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_421;
goto accept0_249;
/*
 * DFA STATE 205
 * ';' -> 422
 * 'v' -> 423
 */
state0_205:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_422;
}
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_423;
goto accept0_249;
/*
 * DFA STATE 206
 * 'u' -> 424
 */
state0_206:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_424;
goto accept0_249;
/*
 * DFA STATE 207
 * 'u' -> 425
 */
state0_207:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_425;
goto accept0_249;
/*
 * DFA STATE 208
 * 'i' -> 426
 * 'o' -> 427
 */
state0_208:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x69) /* ('h') 'i' */ 
        goto accept0_249;
    goto state0_426;
}
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_427;
goto accept0_249;
/*
 * DFA STATE 209
 * 'i' -> 428
 */
state0_209:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_428;
goto accept0_249;
/*
 * DFA STATE 210
 * 'o' -> 429
 */
state0_210:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_429;
goto accept0_249;
/*
 * DFA STATE 211
 * 'd' -> 430
 * 'q' -> 431
 * 'r' -> 432
 */
state0_211:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x64) /* ('c') 'd' */ 
        goto accept0_249;
    if(current < 0x65) /* ('d') 'e' */ 
        goto state0_430;
    goto accept0_249;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_431;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_432;
goto accept0_249;
/*
 * DFA STATE 212
 * 'e' -> 433
 */
state0_212:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_433;
goto accept0_249;
/*
 * DFA STATE 213
 * 'q' -> 434
 */
state0_213:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_434;
goto accept0_249;
/*
 * DFA STATE 214
 * 'g' -> 435
 */
state0_214:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_435;
goto accept0_249;
/*
 * DFA STATE 215
 * 'l' -> 436
 */
state0_215:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_436;
goto accept0_249;
/*
 * DFA STATE 216
 * 'o' -> 437
 */
state0_216:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_437;
goto accept0_249;
/*
 * DFA STATE 217
 * 'm' -> 438
 */
state0_217:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_438;
goto accept0_249;
/*
 * DFA STATE 218
 * 'a' -> 439
 * 'q' -> 440
 */
state0_218:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_439;
}
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_440;
goto accept0_249;
/*
 * DFA STATE 219
 * 'q' -> 441
 */
state0_219:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_441;
goto accept0_249;
/*
 * DFA STATE 220
 * 'a' -> 442
 */
state0_220:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_442;
goto accept0_249;
/*
 * DFA STATE 221
 * 'o' -> 443
 */
state0_221:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_443;
goto accept0_249;
/*
 * DFA STATE 222
 * 'c' -> 444
 */
state0_222:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_444;
goto accept0_249;
/*
 * DFA STATE 223
 * 'y' -> 445
 */
state0_223:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x79) /* ('x') 'y' */ 
    goto accept0_249;
if(current < 0x7A) /* ('y') 'z' */ 
    goto state0_445;
goto accept0_249;
/*
 * DFA STATE 224
 * 'g' -> 446
 * 'm' -> 447
 */
state0_224:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x67) /* ('f') 'g' */ 
        goto accept0_249;
    goto state0_446;
}
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_447;
goto accept0_249;
/*
 * DFA STATE 225
 * 'a' -> 448
 */
state0_225:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_448;
goto accept0_249;
/*
 * DFA STATE 226
 * 'b' -> 449
 * 'm' -> 450
 * 'p' -> 451
 */
state0_226:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */  {
    if(current < 0x62) /* ('a') 'b' */ 
        goto accept0_249;
    if(current < 0x63) /* ('b') 'c' */ 
        goto state0_449;
    goto accept0_249;
}
if(current < 0x70) /* ('o') 'p' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state0_450;
    goto accept0_249;
}
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_451;
goto accept0_249;
/*
 * DFA STATE 227
 * 'l' -> 452
 */
state0_227:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_452;
goto accept0_249;
/*
 * DFA STATE 228
 * 'u' -> 453
 */
state0_228:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_453;
goto accept0_249;
/*
 * DFA STATE 229
 * 'e' -> 454
 * 'i' -> 455
 * 'o' -> 456
 */
state0_229:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x65) /* ('d') 'e' */ 
        goto accept0_249;
    if(current < 0x66) /* ('e') 'f' */ 
        goto state0_454;
    goto accept0_249;
}
if(current < 0x6F) /* ('n') 'o' */  {
    if(current < 0x6A) /* ('i') 'j' */ 
        goto state0_455;
    goto accept0_249;
}
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_456;
goto accept0_249;
/*
 * DFA STATE 230
 * 'l' -> 457
 * 'm' -> 458
 */
state0_230:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */  {
    if(current < 0x6C) /* ('k') 'l' */ 
        goto accept0_249;
    goto state0_457;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_458;
goto accept0_249;
/*
 * DFA STATE 231
 * 'a' -> 459
 */
state0_231:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_459;
goto accept0_249;
/*
 * DFA STATE 232
 * 'c' -> 460
 * 'r' -> 461
 */
state0_232:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */  {
    if(current < 0x63) /* ('b') 'c' */ 
        goto accept0_249;
    goto state0_460;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_461;
goto accept0_249;
/*
 * DFA STATE 233
 * 'i' -> 462
 */
state0_233:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_462;
goto accept0_249;
/*
 * DFA STATE 234
 * 'r' -> 463
 */
state0_234:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_463;
goto accept0_249;
/*
 * DFA STATE 235
 * 'l' -> 464
 */
state0_235:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_464;
goto accept0_249;
/*
 * DFA STATE 236
 * 's' -> 465
 */
state0_236:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_465;
goto accept0_249;
/*
 * DFA STATE 237
 * 'm' -> 466
 */
state0_237:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_466;
goto accept0_249;
/*
 * DFA STATE 238
 * ';' -> 467
 */
state0_238:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_467;
goto accept0_249;
/*
 * DFA STATE 239
 * 'c' -> 468
 */
state0_239:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_468;
goto accept0_249;
/*
 * DFA STATE 240
 * 'n' -> 469
 */
state0_240:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_469;
goto accept0_249;
/*
 * DFA STATE 241
 * 'm' -> 470
 */
state0_241:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_470;
goto accept0_249;
/*
 * DFA STATE 242
 * 't' -> 471
 */
state0_242:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_471;
goto accept0_249;
/*
 * DFA STATE 243
 * 'j' -> 472
 * 'n' -> 473
 */
state0_243:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6B) /* ('j') 'k' */  {
    if(current < 0x6A) /* ('i') 'j' */ 
        goto accept0_249;
    goto state0_472;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_473;
goto accept0_249;
/*
 * DFA STATE 244
 * 'C' -> 474
 */
state0_244:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x43) /* ('B') 'C' */ 
    goto accept0_3;
if(current < 0x44) /* ('C') 'D' */ 
    goto state0_474;
goto accept0_3;
/*
 * DFA STATE 245 (accepts to 0)
 */
state0_245:
pEnd = pNext;
goto accept0_0;
/*
 * DFA STATE 246 (accepts to 5)
 * '-' -> 475
 * ['0'-'9'] -> 475
 * ':' -> 475
 * ['A'-'Z'] -> 475
 * ['a'-'z'] -> 475
 */
state0_246:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_5;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_5;
        goto state0_475;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_5;
    goto state0_475;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_5;
    goto state0_475;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_5;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_475;
goto accept0_5;
/*
 * DFA STATE 247 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'Z'] -> 247
 * ['a'-'z'] -> 247
 */
state0_247:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        goto state0_247;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_6;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 248 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'Q'] -> 247
 * 'R' -> 476
 * ['S'-'Z'] -> 247
 * ['a'-'q'] -> 247
 * 'r' -> 476
 * ['s'-'z'] -> 247
 */
state0_248:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x52) /* ('Q') 'R' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_247;
        goto accept0_6;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto state0_247;
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x53) /* ('R') 'S' */ 
        goto state0_476;
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_247;
    goto accept0_6;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto state0_247;
    goto state0_476;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 249 (accepts to 7)
 */
state0_249:
pEnd = pNext;
goto accept0_7;
/*
 * DFA STATE 250
 * ['0'-'9'] -> 250
 * ';' -> 477
 * ['A'-'F'] -> 250
 * ['a'-'f'] -> 250
 */
state0_250:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3A) /* ('9') ':' */  {
        if(current < 0x30) /* ('/') '0' */ 
            goto accept0_249;
        goto state0_250;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_477;
}
if(current < 0x47) /* ('F') 'G' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_249;
    goto state0_250;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x67) /* ('f') 'g' */ 
    goto state0_250;
goto accept0_249;
/*
 * DFA STATE 251
 * 'i' -> 478
 */
state0_251:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_478;
goto accept0_249;
/*
 * DFA STATE 252
 * 'u' -> 479
 */
state0_252:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_479;
goto accept0_249;
/*
 * DFA STATE 253
 * 'r' -> 480
 */
state0_253:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_480;
goto accept0_249;
/*
 * DFA STATE 254
 * 'a' -> 481
 */
state0_254:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_481;
goto accept0_249;
/*
 * DFA STATE 255
 * 'h' -> 482
 */
state0_255:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_482;
goto accept0_249;
/*
 * DFA STATE 256
 * 'n' -> 483
 */
state0_256:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_483;
goto accept0_249;
/*
 * DFA STATE 257
 * 'l' -> 484
 */
state0_257:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_484;
goto accept0_249;
/*
 * DFA STATE 258
 * 'l' -> 485
 */
state0_258:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_485;
goto accept0_249;
/*
 * DFA STATE 259
 * 'a' -> 486
 */
state0_259:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_486;
goto accept0_249;
/*
 * DFA STATE 260
 * 'd' -> 487
 */
state0_260:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_487;
goto accept0_249;
/*
 * DFA STATE 261
 * ';' -> 488
 */
state0_261:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_488;
goto accept0_249;
/*
 * DFA STATE 262
 * 'g' -> 489
 */
state0_262:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_489;
goto accept0_249;
/*
 * DFA STATE 263
 * 't' -> 490
 */
state0_263:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_490;
goto accept0_249;
/*
 * DFA STATE 264
 * ';' -> 491
 */
state0_264:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_491;
goto accept0_249;
/*
 * DFA STATE 265
 * 'u' -> 492
 */
state0_265:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_492;
goto accept0_249;
/*
 * DFA STATE 266
 * 'r' -> 493
 */
state0_266:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_493;
goto accept0_249;
/*
 * DFA STATE 267
 * 'a' -> 494
 */
state0_267:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_494;
goto accept0_249;
/*
 * DFA STATE 268
 * 'i' -> 495
 */
state0_268:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_495;
goto accept0_249;
/*
 * DFA STATE 269
 * ';' -> 496
 */
state0_269:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_496;
goto accept0_249;
/*
 * DFA STATE 270
 * 'l' -> 497
 */
state0_270:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_497;
goto accept0_249;
/*
 * DFA STATE 271
 * 'm' -> 498
 */
state0_271:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_498;
goto accept0_249;
/*
 * DFA STATE 272
 * 'u' -> 499
 */
state0_272:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_499;
goto accept0_249;
/*
 * DFA STATE 273
 * 'r' -> 500
 */
state0_273:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_500;
goto accept0_249;
/*
 * DFA STATE 274
 * 'a' -> 501
 */
state0_274:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_501;
goto accept0_249;
/*
 * DFA STATE 275
 * 'a' -> 502
 */
state0_275:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_502;
goto accept0_249;
/*
 * DFA STATE 276
 * 'l' -> 503
 */
state0_276:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_503;
goto accept0_249;
/*
 * DFA STATE 277
 * 'p' -> 504
 */
state0_277:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_504;
goto accept0_249;
/*
 * DFA STATE 278
 * 'b' -> 505
 */
state0_278:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_505;
goto accept0_249;
/*
 * DFA STATE 279 (accepts to 120)
 */
state0_279:
pEnd = pNext;
goto accept0_120;
/*
 * DFA STATE 280
 * 'l' -> 506
 */
state0_280:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_506;
goto accept0_249;
/*
 * DFA STATE 281 (accepts to 121)
 */
state0_281:
pEnd = pNext;
goto accept0_121;
/*
 * DFA STATE 282
 * 'i' -> 507
 */
state0_282:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_507;
goto accept0_249;
/*
 * DFA STATE 283
 * 'u' -> 508
 */
state0_283:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_508;
goto accept0_249;
/*
 * DFA STATE 284
 * 'r' -> 509
 */
state0_284:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_509;
goto accept0_249;
/*
 * DFA STATE 285
 * 'a' -> 510
 */
state0_285:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_510;
goto accept0_249;
/*
 * DFA STATE 286
 * 'g' -> 511
 */
state0_286:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_511;
goto accept0_249;
/*
 * DFA STATE 287
 * 'c' -> 512
 */
state0_287:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_512;
goto accept0_249;
/*
 * DFA STATE 288
 * 'a' -> 513
 */
state0_288:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_513;
goto accept0_249;
/*
 * DFA STATE 289
 * 'l' -> 514
 */
state0_289:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_514;
goto accept0_249;
/*
 * DFA STATE 290
 * 'l' -> 515
 */
state0_290:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_515;
goto accept0_249;
/*
 * DFA STATE 291
 * ';' -> 516
 */
state0_291:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_516;
goto accept0_249;
/*
 * DFA STATE 292 (accepts to 124)
 */
state0_292:
pEnd = pNext;
goto accept0_124;
/*
 * DFA STATE 293
 * 'm' -> 517
 */
state0_293:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_517;
goto accept0_249;
/*
 * DFA STATE 294
 * ';' -> 518
 */
state0_294:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_518;
goto accept0_249;
/*
 * DFA STATE 295
 * ';' -> 519
 */
state0_295:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_519;
goto accept0_249;
/*
 * DFA STATE 296
 * 'r' -> 520
 */
state0_296:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_520;
goto accept0_249;
/*
 * DFA STATE 297
 * 'm' -> 521
 */
state0_297:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_521;
goto accept0_249;
/*
 * DFA STATE 298
 * 'R' -> 522
 */
state0_298:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x52) /* ('Q') 'R' */ 
    goto accept0_249;
if(current < 0x53) /* ('R') 'S' */ 
    goto state0_522;
goto accept0_249;
/*
 * DFA STATE 299
 * ';' -> 523
 */
state0_299:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_523;
goto accept0_249;
/*
 * DFA STATE 300
 * 't' -> 524
 */
state0_300:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_524;
goto accept0_249;
/*
 * DFA STATE 301
 * 'u' -> 525
 */
state0_301:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_525;
goto accept0_249;
/*
 * DFA STATE 302
 * 'r' -> 526
 */
state0_302:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_526;
goto accept0_249;
/*
 * DFA STATE 303
 * 'a' -> 527
 */
state0_303:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_527;
goto accept0_249;
/*
 * DFA STATE 304
 * 'i' -> 528
 */
state0_304:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_528;
goto accept0_249;
/*
 * DFA STATE 305
 * 'l' -> 529
 */
state0_305:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_529;
goto accept0_249;
/*
 * DFA STATE 306 (accepts to 122)
 */
state0_306:
pEnd = pNext;
goto accept0_122;
/*
 * DFA STATE 307
 * 'u' -> 530
 */
state0_307:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_530;
goto accept0_249;
/*
 * DFA STATE 308
 * 'l' -> 531
 */
state0_308:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_531;
goto accept0_249;
/*
 * DFA STATE 309
 * 'a' -> 532
 */
state0_309:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_532;
goto accept0_249;
/*
 * DFA STATE 310
 * 'u' -> 533
 */
state0_310:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_533;
goto accept0_249;
/*
 * DFA STATE 311
 * 'r' -> 534
 */
state0_311:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_534;
goto accept0_249;
/*
 * DFA STATE 312
 * 't' -> 535
 */
state0_312:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_535;
goto accept0_249;
/*
 * DFA STATE 313
 * 'i' -> 536
 */
state0_313:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_536;
goto accept0_249;
/*
 * DFA STATE 314
 * 'a' -> 537
 */
state0_314:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_537;
goto accept0_249;
/*
 * DFA STATE 315
 * 'h' -> 538
 */
state0_315:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_538;
goto accept0_249;
/*
 * DFA STATE 316
 * ';' -> 539
 */
state0_316:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_539;
goto accept0_249;
/*
 * DFA STATE 317
 * ';' -> 540
 */
state0_317:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_540;
goto accept0_249;
/*
 * DFA STATE 318
 * ';' -> 541
 */
state0_318:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_541;
goto accept0_249;
/*
 * DFA STATE 319
 * 'n' -> 542
 */
state0_319:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_542;
goto accept0_249;
/*
 * DFA STATE 320
 * 'm' -> 543
 */
state0_320:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_543;
goto accept0_249;
/*
 * DFA STATE 321
 * 'l' -> 544
 */
state0_321:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_544;
goto accept0_249;
/*
 * DFA STATE 322
 * 'l' -> 545
 */
state0_322:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_545;
goto accept0_249;
/*
 * DFA STATE 323
 * 'u' -> 546
 */
state0_323:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_546;
goto accept0_249;
/*
 * DFA STATE 324
 * 'a' -> 547
 */
state0_324:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_547;
goto accept0_249;
/*
 * DFA STATE 325
 * 'b' -> 548
 */
state0_325:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_548;
goto accept0_249;
/*
 * DFA STATE 326
 * 'l' -> 549
 */
state0_326:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_549;
goto accept0_249;
/*
 * DFA STATE 327
 * ';' -> 550
 */
state0_327:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_550;
goto accept0_249;
/*
 * DFA STATE 328
 * 'd' -> 551
 */
state0_328:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_551;
goto accept0_249;
/*
 * DFA STATE 329
 * 'i' -> 552
 */
state0_329:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_552;
goto accept0_249;
/*
 * DFA STATE 330
 * 't' -> 553
 */
state0_330:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_553;
goto accept0_249;
/*
 * DFA STATE 331
 * ';' -> 554
 */
state0_331:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_554;
goto accept0_249;
/*
 * DFA STATE 332
 * 'c' -> 555
 */
state0_332:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_555;
goto accept0_249;
/*
 * DFA STATE 333
 * 'b' -> 556
 */
state0_333:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_556;
goto accept0_249;
/*
 * DFA STATE 334
 * 'g' -> 557
 */
state0_334:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_557;
goto accept0_249;
/*
 * DFA STATE 335
 * 'y' -> 558
 */
state0_335:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x79) /* ('x') 'y' */ 
    goto accept0_249;
if(current < 0x7A) /* ('y') 'z' */ 
    goto state0_558;
goto accept0_249;
/*
 * DFA STATE 336
 * 'r' -> 559
 */
state0_336:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_559;
goto accept0_249;
/*
 * DFA STATE 337
 * ';' -> 560
 */
state0_337:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_560;
goto accept0_249;
/*
 * DFA STATE 338
 * 'r' -> 561
 */
state0_338:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_561;
goto accept0_249;
/*
 * DFA STATE 339
 * 'g' -> 562
 */
state0_339:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_562;
goto accept0_249;
/*
 * DFA STATE 340
 * 'r' -> 563
 */
state0_340:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_563;
goto accept0_249;
/*
 * DFA STATE 341
 * ';' -> 564
 */
state0_341:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_564;
goto accept0_249;
/*
 * DFA STATE 342
 * 't' -> 565
 */
state0_342:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_565;
goto accept0_249;
/*
 * DFA STATE 343
 * 'm' -> 566
 */
state0_343:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_566;
goto accept0_249;
/*
 * DFA STATE 344
 * 'i' -> 567
 */
state0_344:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_567;
goto accept0_249;
/*
 * DFA STATE 345
 * 'u' -> 568
 */
state0_345:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_568;
goto accept0_249;
/*
 * DFA STATE 346
 * 'r' -> 569
 */
state0_346:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_569;
goto accept0_249;
/*
 * DFA STATE 347
 * 'a' -> 570
 */
state0_347:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_570;
goto accept0_249;
/*
 * DFA STATE 348
 * 't' -> 571
 */
state0_348:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_571;
goto accept0_249;
/*
 * DFA STATE 349
 * 'p' -> 572
 */
state0_349:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_572;
goto accept0_249;
/*
 * DFA STATE 350
 * 'p' -> 573
 */
state0_350:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_573;
goto accept0_249;
/*
 * DFA STATE 351
 * 'i' -> 574
 */
state0_351:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_574;
goto accept0_249;
/*
 * DFA STATE 352
 * 'i' -> 575
 */
state0_352:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_575;
goto accept0_249;
/*
 * DFA STATE 353
 * ';' -> 576
 */
state0_353:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_576;
goto accept0_249;
/*
 * DFA STATE 354
 * ';' -> 577
 */
state0_354:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_577;
goto accept0_249;
/*
 * DFA STATE 355
 * 'l' -> 578
 */
state0_355:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_578;
goto accept0_249;
/*
 * DFA STATE 356
 * 'o' -> 579
 */
state0_356:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_579;
goto accept0_249;
/*
 * DFA STATE 357
 * 's' -> 580
 */
state0_357:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_580;
goto accept0_249;
/*
 * DFA STATE 358
 * 'f' -> 581
 */
state0_358:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x66) /* ('e') 'f' */ 
    goto accept0_249;
if(current < 0x67) /* ('f') 'g' */ 
    goto state0_581;
goto accept0_249;
/*
 * DFA STATE 359
 * 'a' -> 582
 */
state0_359:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_582;
goto accept0_249;
/*
 * DFA STATE 360
 * 'c' -> 583
 */
state0_360:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_583;
goto accept0_249;
/*
 * DFA STATE 361
 * 'm' -> 584
 */
state0_361:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_584;
goto accept0_249;
/*
 * DFA STATE 362 (accepts to 189)
 */
state0_362:
pEnd = pNext;
goto accept0_189;
/*
 * DFA STATE 363 (accepts to 12)
 */
state0_363:
pEnd = pNext;
goto accept0_12;
/*
 * DFA STATE 364
 * 'r' -> 585
 */
state0_364:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_585;
goto accept0_249;
/*
 * DFA STATE 365
 * 'r' -> 586
 */
state0_365:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_586;
goto accept0_249;
/*
 * DFA STATE 366
 * 'l' -> 587
 */
state0_366:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_587;
goto accept0_249;
/*
 * DFA STATE 367
 * 'u' -> 588
 */
state0_367:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_588;
goto accept0_249;
/*
 * DFA STATE 368
 * 'r' -> 589
 */
state0_368:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_589;
goto accept0_249;
/*
 * DFA STATE 369
 * 'c' -> 590
 */
state0_369:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_590;
goto accept0_249;
/*
 * DFA STATE 370
 * 'a' -> 591
 */
state0_370:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_591;
goto accept0_249;
/*
 * DFA STATE 371
 * 'i' -> 592
 */
state0_371:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_592;
goto accept0_249;
/*
 * DFA STATE 372
 * ';' -> 593
 */
state0_372:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_593;
goto accept0_249;
/*
 * DFA STATE 373
 * 'a' -> 594
 */
state0_373:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_594;
goto accept0_249;
/*
 * DFA STATE 374
 * 'e' -> 595
 */
state0_374:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_595;
goto accept0_249;
/*
 * DFA STATE 375
 * 'n' -> 596
 */
state0_375:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_596;
goto accept0_249;
/*
 * DFA STATE 376
 * 'l' -> 597
 */
state0_376:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_597;
goto accept0_249;
/*
 * DFA STATE 377
 * 'p' -> 598
 */
state0_377:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_598;
goto accept0_249;
/*
 * DFA STATE 378
 * 'b' -> 599
 */
state0_378:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_599;
goto accept0_249;
/*
 * DFA STATE 379
 * 'u' -> 600
 */
state0_379:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_600;
goto accept0_249;
/*
 * DFA STATE 380
 * 'r' -> 601
 */
state0_380:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_601;
goto accept0_249;
/*
 * DFA STATE 381
 * 'i' -> 602
 */
state0_381:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_602;
goto accept0_249;
/*
 * DFA STATE 382
 * 'u' -> 603
 */
state0_382:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_603;
goto accept0_249;
/*
 * DFA STATE 383 (accepts to 188)
 */
state0_383:
pEnd = pNext;
goto accept0_188;
/*
 * DFA STATE 384
 * 'o' -> 604
 */
state0_384:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_604;
goto accept0_249;
/*
 * DFA STATE 385
 * 'a' -> 605
 */
state0_385:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_605;
goto accept0_249;
/*
 * DFA STATE 386
 * ';' -> 606
 */
state0_386:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_606;
goto accept0_249;
/*
 * DFA STATE 387
 * ';' -> 607
 */
state0_387:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_607;
goto accept0_249;
/*
 * DFA STATE 388
 * 'q' -> 608
 */
state0_388:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_608;
goto accept0_249;
/*
 * DFA STATE 389
 * 'u' -> 609
 */
state0_389:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_609;
goto accept0_249;
/*
 * DFA STATE 390 (accepts to 11)
 */
state0_390:
pEnd = pNext;
goto accept0_11;
/*
 * DFA STATE 391
 * 'r' -> 610
 */
state0_391:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_610;
goto accept0_249;
/*
 * DFA STATE 392
 * 's' -> 611
 */
state0_392:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_611;
goto accept0_249;
/*
 * DFA STATE 393
 * 'r' -> 612
 */
state0_393:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_612;
goto accept0_249;
/*
 * DFA STATE 394
 * 'd' -> 613
 */
state0_394:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_613;
goto accept0_249;
/*
 * DFA STATE 395
 * 'u' -> 614
 */
state0_395:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_614;
goto accept0_249;
/*
 * DFA STATE 396 (accepts to 144)
 */
state0_396:
pEnd = pNext;
goto accept0_144;
/*
 * DFA STATE 397
 * 'l' -> 615
 */
state0_397:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_615;
goto accept0_249;
/*
 * DFA STATE 398
 * 'p' -> 616
 */
state0_398:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_616;
goto accept0_249;
/*
 * DFA STATE 399
 * 's' -> 617
 */
state0_399:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_617;
goto accept0_249;
/*
 * DFA STATE 400 (accepts to 186)
 */
state0_400:
pEnd = pNext;
goto accept0_186;
/*
 * DFA STATE 401 (accepts to 168)
 */
state0_401:
pEnd = pNext;
goto accept0_168;
/*
 * DFA STATE 402
 * ';' -> 618
 * 'i' -> 619
 */
state0_402:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_618;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_619;
goto accept0_249;
/*
 * DFA STATE 403
 * 'b' -> 620
 */
state0_403:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */ 
    goto accept0_249;
if(current < 0x63) /* ('b') 'c' */ 
    goto state0_620;
goto accept0_249;
/*
 * DFA STATE 404
 * 'l' -> 621
 */
state0_404:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_621;
goto accept0_249;
/*
 * DFA STATE 405 (accepts to 145)
 */
state0_405:
pEnd = pNext;
goto accept0_145;
/*
 * DFA STATE 406
 * 'u' -> 622
 */
state0_406:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_622;
goto accept0_249;
/*
 * DFA STATE 407
 * 'r' -> 623
 */
state0_407:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_623;
goto accept0_249;
/*
 * DFA STATE 408
 * 'i' -> 624
 */
state0_408:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_624;
goto accept0_249;
/*
 * DFA STATE 409
 * 'a' -> 625
 */
state0_409:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_625;
goto accept0_249;
/*
 * DFA STATE 410
 * 'n' -> 626
 */
state0_410:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_626;
goto accept0_249;
/*
 * DFA STATE 411
 * 'g' -> 627
 */
state0_411:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_627;
goto accept0_249;
/*
 * DFA STATE 412
 * 'c' -> 628
 */
state0_412:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_628;
goto accept0_249;
/*
 * DFA STATE 413
 * 'u' -> 629
 */
state0_413:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_629;
goto accept0_249;
/*
 * DFA STATE 414 (accepts to 178)
 */
state0_414:
pEnd = pNext;
goto accept0_178;
/*
 * DFA STATE 415
 * 'f' -> 630
 * 'm' -> 631
 */
state0_415:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto accept0_249;
    goto state0_630;
}
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_631;
goto accept0_249;
/*
 * DFA STATE 416
 * 'a' -> 632
 */
state0_416:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_632;
goto accept0_249;
/*
 * DFA STATE 417
 * 'l' -> 633
 * 'm' -> 634
 */
state0_417:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */  {
    if(current < 0x6C) /* ('k') 'l' */ 
        goto accept0_249;
    goto state0_633;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_634;
goto accept0_249;
/*
 * DFA STATE 418
 * 'l' -> 635
 */
state0_418:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_635;
goto accept0_249;
/*
 * DFA STATE 419
 * 'a' -> 636
 * 't' -> 637
 */
state0_419:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x62) /* ('a') 'b' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept0_249;
    goto state0_636;
}
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_637;
goto accept0_249;
/*
 * DFA STATE 420
 * 'm' -> 638
 * 'p' -> 639
 */
state0_420:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */  {
    if(current < 0x6D) /* ('l') 'm' */ 
        goto accept0_249;
    goto state0_638;
}
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_639;
goto accept0_249;
/*
 * DFA STATE 421
 * ';' -> 640
 */
state0_421:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_640;
goto accept0_249;
/*
 * DFA STATE 422 (accepts to 148)
 */
state0_422:
pEnd = pNext;
goto accept0_148;
/*
 * DFA STATE 423
 * ';' -> 641
 */
state0_423:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_641;
goto accept0_249;
/*
 * DFA STATE 424
 * 's' -> 642
 */
state0_424:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_642;
goto accept0_249;
/*
 * DFA STATE 425
 * 'n' -> 643
 */
state0_425:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_643;
goto accept0_249;
/*
 * DFA STATE 426
 * 'm' -> 644
 */
state0_426:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_644;
goto accept0_249;
/*
 * DFA STATE 427
 * 'd' -> 645
 * 'p' -> 646
 */
state0_427:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x64) /* ('c') 'd' */ 
        goto accept0_249;
    goto state0_645;
}
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_646;
goto accept0_249;
/*
 * DFA STATE 428
 * ';' -> 647
 */
state0_428:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_647;
goto accept0_249;
/*
 * DFA STATE 429
 * 't' -> 648
 */
state0_429:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_648;
goto accept0_249;
/*
 * DFA STATE 430
 * 'i' -> 649
 */
state0_430:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_649;
goto accept0_249;
/*
 * DFA STATE 431
 * 'u' -> 650
 */
state0_431:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_650;
goto accept0_249;
/*
 * DFA STATE 432
 * 'r' -> 651
 */
state0_432:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_651;
goto accept0_249;
/*
 * DFA STATE 433
 * 'i' -> 652
 */
state0_433:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_652;
goto accept0_249;
/*
 * DFA STATE 434
 * 'u' -> 653
 */
state0_434:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_653;
goto accept0_249;
/*
 * DFA STATE 435
 * ';' -> 654
 */
state0_435:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_654;
goto accept0_249;
/*
 * DFA STATE 436
 * 'o' -> 655
 */
state0_436:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_655;
goto accept0_249;
/*
 * DFA STATE 437
 * ';' -> 656
 */
state0_437:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_656;
goto accept0_249;
/*
 * DFA STATE 438
 * ';' -> 657
 */
state0_438:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_657;
goto accept0_249;
/*
 * DFA STATE 439
 * 'q' -> 658
 */
state0_439:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x71) /* ('p') 'q' */ 
    goto accept0_249;
if(current < 0x72) /* ('q') 'r' */ 
    goto state0_658;
goto accept0_249;
/*
 * DFA STATE 440
 * 'u' -> 659
 */
state0_440:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_659;
goto accept0_249;
/*
 * DFA STATE 441
 * 'u' -> 660
 */
state0_441:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_660;
goto accept0_249;
/*
 * DFA STATE 442
 * 'r' -> 661
 */
state0_442:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_661;
goto accept0_249;
/*
 * DFA STATE 443
 * 't' -> 662
 */
state0_443:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_662;
goto accept0_249;
/*
 * DFA STATE 444
 * 't' -> 663
 */
state0_444:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_663;
goto accept0_249;
/*
 * DFA STATE 445
 * ';' -> 664
 */
state0_445:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_664;
goto accept0_249;
/*
 * DFA STATE 446
 * 'm' -> 665
 */
state0_446:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_665;
goto accept0_249;
/*
 * DFA STATE 447
 * ';' -> 666
 */
state0_447:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_666;
goto accept0_249;
/*
 * DFA STATE 448
 * 'd' -> 667
 */
state0_448:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_667;
goto accept0_249;
/*
 * DFA STATE 449
 * ';' -> 668
 * 'e' -> 669
 */
state0_449:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_668;
}
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_669;
goto accept0_249;
/*
 * DFA STATE 450
 * ';' -> 670
 */
state0_450:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_670;
goto accept0_249;
/*
 * DFA STATE 451
 * '1' -> 671
 * '2' -> 672
 * '3' -> 673
 * ';' -> 674
 * 'e' -> 675
 */
state0_451:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x34) /* ('3') '4' */  {
    if(current < 0x32) /* ('1') '2' */  {
        if(current < 0x31) /* ('0') '1' */ 
            goto accept0_249;
        goto state0_671;
    }
    if(current < 0x33) /* ('2') '3' */ 
        goto state0_672;
    goto state0_673;
}
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_674;
}
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_675;
goto accept0_249;
/*
 * DFA STATE 452
 * 'i' -> 676
 */
state0_452:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_676;
goto accept0_249;
/*
 * DFA STATE 453
 * ';' -> 677
 */
state0_453:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_677;
goto accept0_249;
/*
 * DFA STATE 454
 * 'r' -> 678
 * 't' -> 679
 */
state0_454:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto accept0_249;
    goto state0_678;
}
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_679;
goto accept0_249;
/*
 * DFA STATE 455
 * 'n' -> 680
 */
state0_455:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_680;
goto accept0_249;
/*
 * DFA STATE 456
 * 'r' -> 681
 */
state0_456:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_681;
goto accept0_249;
/*
 * DFA STATE 457
 * 'd' -> 682
 */
state0_457:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_682;
goto accept0_249;
/*
 * DFA STATE 458
 * 'e' -> 683
 */
state0_458:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_683;
goto accept0_249;
/*
 * DFA STATE 459
 * 'd' -> 684
 */
state0_459:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_684;
goto accept0_249;
/*
 * DFA STATE 460
 * 'u' -> 685
 */
state0_460:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_685;
goto accept0_249;
/*
 * DFA STATE 461
 * 'r' -> 686
 */
state0_461:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_686;
goto accept0_249;
/*
 * DFA STATE 462
 * 'r' -> 687
 */
state0_462:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_687;
goto accept0_249;
/*
 * DFA STATE 463
 * 'a' -> 688
 */
state0_463:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_688;
goto accept0_249;
/*
 * DFA STATE 464
 * ';' -> 689
 */
state0_464:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_689;
goto accept0_249;
/*
 * DFA STATE 465
 * 'i' -> 690
 */
state0_465:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_690;
goto accept0_249;
/*
 * DFA STATE 466
 * 'l' -> 691
 */
state0_466:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_691;
goto accept0_249;
/*
 * DFA STATE 467 (accepts to 146)
 */
state0_467:
pEnd = pNext;
goto accept0_146;
/*
 * DFA STATE 468
 * 'u' -> 692
 */
state0_468:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_692;
goto accept0_249;
/*
 * DFA STATE 469
 * ';' -> 693
 */
state0_469:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_693;
goto accept0_249;
/*
 * DFA STATE 470
 * 'l' -> 694
 */
state0_470:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_694;
goto accept0_249;
/*
 * DFA STATE 471
 * 'a' -> 695
 */
state0_471:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_695;
goto accept0_249;
/*
 * DFA STATE 472
 * ';' -> 696
 */
state0_472:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_696;
goto accept0_249;
/*
 * DFA STATE 473
 * 'j' -> 697
 */
state0_473:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6A) /* ('i') 'j' */ 
    goto accept0_249;
if(current < 0x6B) /* ('j') 'k' */ 
    goto state0_697;
goto accept0_249;
/*
 * DFA STATE 474
 * 'D' -> 698
 */
state0_474:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x44) /* ('C') 'D' */ 
    goto accept0_3;
if(current < 0x45) /* ('D') 'E' */ 
    goto state0_698;
goto accept0_3;
/*
 * DFA STATE 475 (accepts to 5)
 * '-' -> 475
 * ['0'-'9'] -> 475
 * ':' -> 475
 * ['A'-'Z'] -> 475
 * ['a'-'z'] -> 475
 */
state0_475:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_5;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_5;
        goto state0_475;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_5;
    goto state0_475;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_5;
    goto state0_475;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_5;
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_475;
goto accept0_5;
/*
 * DFA STATE 476 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'H'] -> 247
 * 'I' -> 699
 * ['J'-'Z'] -> 247
 * ['a'-'h'] -> 247
 * 'i' -> 699
 * ['j'-'z'] -> 247
 */
state0_476:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x49) /* ('H') 'I' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_247;
        goto accept0_6;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto state0_247;
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x4A) /* ('I') 'J' */ 
        goto state0_699;
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_247;
    goto accept0_6;
}
if(current < 0x6A) /* ('i') 'j' */  {
    if(current < 0x69) /* ('h') 'i' */ 
        goto state0_247;
    goto state0_699;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 477 (accepts to 8)
 */
state0_477:
pEnd = pNext;
goto accept0_8;
/*
 * DFA STATE 478
 * 'g' -> 700
 */
state0_478:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_700;
goto accept0_249;
/*
 * DFA STATE 479
 * 't' -> 701
 */
state0_479:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_701;
goto accept0_249;
/*
 * DFA STATE 480
 * 'c' -> 702
 */
state0_480:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_702;
goto accept0_249;
/*
 * DFA STATE 481
 * 'v' -> 703
 */
state0_481:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_703;
goto accept0_249;
/*
 * DFA STATE 482
 * 'a' -> 704
 */
state0_482:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_704;
goto accept0_249;
/*
 * DFA STATE 483
 * 'g' -> 705
 */
state0_483:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_705;
goto accept0_249;
/*
 * DFA STATE 484
 * 'd' -> 706
 */
state0_484:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_706;
goto accept0_249;
/*
 * DFA STATE 485
 * ';' -> 707
 */
state0_485:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_707;
goto accept0_249;
/*
 * DFA STATE 486
 * ';' -> 708
 */
state0_486:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_708;
goto accept0_249;
/*
 * DFA STATE 487
 * 'i' -> 709
 */
state0_487:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_709;
goto accept0_249;
/*
 * DFA STATE 488 (accepts to 130)
 */
state0_488:
pEnd = pNext;
goto accept0_130;
/*
 * DFA STATE 489
 * 'e' -> 710
 */
state0_489:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_710;
goto accept0_249;
/*
 * DFA STATE 490
 * 'a' -> 711
 */
state0_490:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_711;
goto accept0_249;
/*
 * DFA STATE 491 (accepts to 29)
 */
state0_491:
pEnd = pNext;
goto accept0_29;
/*
 * DFA STATE 492
 * 't' -> 712
 */
state0_492:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_712;
goto accept0_249;
/*
 * DFA STATE 493
 * 'c' -> 713
 */
state0_493:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_713;
goto accept0_249;
/*
 * DFA STATE 494
 * 'v' -> 714
 */
state0_494:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_714;
goto accept0_249;
/*
 * DFA STATE 495
 * 'l' -> 715
 */
state0_495:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_715;
goto accept0_249;
/*
 * DFA STATE 496 (accepts to 115)
 */
state0_496:
pEnd = pNext;
goto accept0_115;
/*
 * DFA STATE 497
 * ';' -> 716
 */
state0_497:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_716;
goto accept0_249;
/*
 * DFA STATE 498
 * 'a' -> 717
 */
state0_498:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_717;
goto accept0_249;
/*
 * DFA STATE 499
 * 't' -> 718
 */
state0_499:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_718;
goto accept0_249;
/*
 * DFA STATE 500
 * 'c' -> 719
 */
state0_500:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_719;
goto accept0_249;
/*
 * DFA STATE 501
 * 'v' -> 720
 */
state0_501:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_720;
goto accept0_249;
/*
 * DFA STATE 502
 * ';' -> 721
 */
state0_502:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_721;
goto accept0_249;
/*
 * DFA STATE 503
 * ';' -> 722
 */
state0_503:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_722;
goto accept0_249;
/*
 * DFA STATE 504
 * 'a' -> 723
 */
state0_504:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_723;
goto accept0_249;
/*
 * DFA STATE 505
 * 'd' -> 724
 */
state0_505:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_724;
goto accept0_249;
/*
 * DFA STATE 506
 * 'd' -> 725
 */
state0_506:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_725;
goto accept0_249;
/*
 * DFA STATE 507
 * 'g' -> 726
 */
state0_507:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_726;
goto accept0_249;
/*
 * DFA STATE 508
 * 't' -> 727
 */
state0_508:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_727;
goto accept0_249;
/*
 * DFA STATE 509
 * 'c' -> 728
 */
state0_509:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_728;
goto accept0_249;
/*
 * DFA STATE 510
 * 'v' -> 729
 */
state0_510:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_729;
goto accept0_249;
/*
 * DFA STATE 511
 * 'a' -> 730
 */
state0_511:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_730;
goto accept0_249;
/*
 * DFA STATE 512
 * 'r' -> 731
 */
state0_512:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_731;
goto accept0_249;
/*
 * DFA STATE 513
 * 's' -> 732
 */
state0_513:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_732;
goto accept0_249;
/*
 * DFA STATE 514
 * 'd' -> 733
 */
state0_514:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_733;
goto accept0_249;
/*
 * DFA STATE 515
 * ';' -> 734
 */
state0_515:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_734;
goto accept0_249;
/*
 * DFA STATE 516 (accepts to 129)
 */
state0_516:
pEnd = pNext;
goto accept0_129;
/*
 * DFA STATE 517
 * 'e' -> 735
 */
state0_517:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_735;
goto accept0_249;
/*
 * DFA STATE 518 (accepts to 131)
 */
state0_518:
pEnd = pNext;
goto accept0_131;
/*
 * DFA STATE 519 (accepts to 125)
 */
state0_519:
pEnd = pNext;
goto accept0_125;
/*
 * DFA STATE 520
 * 'o' -> 736
 */
state0_520:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_736;
goto accept0_249;
/*
 * DFA STATE 521
 * 'a' -> 737
 */
state0_521:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_737;
goto accept0_249;
/*
 * DFA STATE 522
 * 'N' -> 738
 */
state0_522:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x4E) /* ('M') 'N' */ 
    goto accept0_249;
if(current < 0x4F) /* ('N') 'O' */ 
    goto state0_738;
goto accept0_249;
/*
 * DFA STATE 523 (accepts to 127)
 */
state0_523:
pEnd = pNext;
goto accept0_127;
/*
 * DFA STATE 524
 * 'a' -> 739
 */
state0_524:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_739;
goto accept0_249;
/*
 * DFA STATE 525
 * 't' -> 740
 */
state0_525:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_740;
goto accept0_249;
/*
 * DFA STATE 526
 * 'c' -> 741
 */
state0_526:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_741;
goto accept0_249;
/*
 * DFA STATE 527
 * 'v' -> 742
 */
state0_527:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_742;
goto accept0_249;
/*
 * DFA STATE 528
 * 'l' -> 743
 */
state0_528:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_743;
goto accept0_249;
/*
 * DFA STATE 529
 * ';' -> 744
 */
state0_529:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_744;
goto accept0_249;
/*
 * DFA STATE 530
 * 't' -> 745
 */
state0_530:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_745;
goto accept0_249;
/*
 * DFA STATE 531
 * ';' -> 746
 */
state0_531:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_746;
goto accept0_249;
/*
 * DFA STATE 532
 * ';' -> 747
 */
state0_532:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_747;
goto accept0_249;
/*
 * DFA STATE 533
 * 't' -> 748
 */
state0_533:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_748;
goto accept0_249;
/*
 * DFA STATE 534
 * 'c' -> 749
 */
state0_534:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_749;
goto accept0_249;
/*
 * DFA STATE 535
 * 'e' -> 750
 */
state0_535:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_750;
goto accept0_249;
/*
 * DFA STATE 536
 * 'g' -> 751
 */
state0_536:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_751;
goto accept0_249;
/*
 * DFA STATE 537
 * 'v' -> 752
 */
state0_537:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_752;
goto accept0_249;
/*
 * DFA STATE 538
 * 'a' -> 753
 */
state0_538:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_753;
goto accept0_249;
/*
 * DFA STATE 539 (accepts to 10)
 */
state0_539:
pEnd = pNext;
goto accept0_10;
/*
 * DFA STATE 540 (accepts to 177)
 */
state0_540:
pEnd = pNext;
goto accept0_177;
/*
 * DFA STATE 541 (accepts to 176)
 */
state0_541:
pEnd = pNext;
goto accept0_176;
/*
 * DFA STATE 542
 * 'g' -> 754
 */
state0_542:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_754;
goto accept0_249;
/*
 * DFA STATE 543
 * 'p' -> 755
 */
state0_543:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_755;
goto accept0_249;
/*
 * DFA STATE 544
 * 'd' -> 756
 */
state0_544:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_756;
goto accept0_249;
/*
 * DFA STATE 545
 * ';' -> 757
 */
state0_545:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_757;
goto accept0_249;
/*
 * DFA STATE 546
 * 'o' -> 758
 */
state0_546:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_758;
goto accept0_249;
/*
 * DFA STATE 547
 * ';' -> 759
 */
state0_547:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_759;
goto accept0_249;
/*
 * DFA STATE 548
 * 'a' -> 760
 */
state0_548:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_760;
goto accept0_249;
/*
 * DFA STATE 549
 * ';' -> 761
 */
state0_549:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_761;
goto accept0_249;
/*
 * DFA STATE 550 (accepts to 179)
 */
state0_550:
pEnd = pNext;
goto accept0_179;
/*
 * DFA STATE 551
 * 'i' -> 762
 */
state0_551:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_762;
goto accept0_249;
/*
 * DFA STATE 552
 * 'l' -> 763
 */
state0_552:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_763;
goto accept0_249;
/*
 * DFA STATE 553
 * ';' -> 764
 */
state0_553:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_764;
goto accept0_249;
/*
 * DFA STATE 554 (accepts to 155)
 */
state0_554:
pEnd = pNext;
goto accept0_155;
/*
 * DFA STATE 555
 * ';' -> 765
 */
state0_555:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_765;
goto accept0_249;
/*
 * DFA STATE 556
 * 's' -> 766
 */
state0_556:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_766;
goto accept0_249;
/*
 * DFA STATE 557
 * ';' -> 767
 */
state0_557:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_767;
goto accept0_249;
/*
 * DFA STATE 558
 * ';' -> 768
 */
state0_558:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_768;
goto accept0_249;
/*
 * DFA STATE 559
 * 'r' -> 769
 */
state0_559:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_769;
goto accept0_249;
/*
 * DFA STATE 560 (accepts to 180)
 */
state0_560:
pEnd = pNext;
goto accept0_180;
/*
 * DFA STATE 561
 * 'e' -> 770
 */
state0_561:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_770;
goto accept0_249;
/*
 * DFA STATE 562
 * 'e' -> 771
 */
state0_562:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_771;
goto accept0_249;
/*
 * DFA STATE 563
 * ';' -> 772
 */
state0_563:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_772;
goto accept0_249;
/*
 * DFA STATE 564 (accepts to 91)
 */
state0_564:
pEnd = pNext;
goto accept0_91;
/*
 * DFA STATE 565
 * 'a' -> 773
 */
state0_565:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_773;
goto accept0_249;
/*
 * DFA STATE 566
 * 's' -> 774
 */
state0_566:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_774;
goto accept0_249;
/*
 * DFA STATE 567
 * 'd' -> 775
 */
state0_567:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_775;
goto accept0_249;
/*
 * DFA STATE 568
 * 't' -> 776
 */
state0_568:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_776;
goto accept0_249;
/*
 * DFA STATE 569
 * 'c' -> 777
 */
state0_569:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_777;
goto accept0_249;
/*
 * DFA STATE 570
 * 'v' -> 778
 */
state0_570:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_778;
goto accept0_249;
/*
 * DFA STATE 571
 * 'y' -> 779
 */
state0_571:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x79) /* ('x') 'y' */ 
    goto accept0_249;
if(current < 0x7A) /* ('y') 'z' */ 
    goto state0_779;
goto accept0_249;
/*
 * DFA STATE 572
 * ';' -> 780
 */
state0_572:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_780;
goto accept0_249;
/*
 * DFA STATE 573
 * ';' -> 781
 */
state0_573:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_781;
goto accept0_249;
/*
 * DFA STATE 574
 * 'l' -> 782
 */
state0_574:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_782;
goto accept0_249;
/*
 * DFA STATE 575
 * 'v' -> 783
 */
state0_575:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_783;
goto accept0_249;
/*
 * DFA STATE 576 (accepts to 139)
 */
state0_576:
pEnd = pNext;
goto accept0_139;
/*
 * DFA STATE 577 (accepts to 60)
 */
state0_577:
pEnd = pNext;
goto accept0_60;
/*
 * DFA STATE 578
 * ';' -> 784
 */
state0_578:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_784;
goto accept0_249;
/*
 * DFA STATE 579
 * ';' -> 785
 */
state0_579:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_785;
goto accept0_249;
/*
 * DFA STATE 580
 * 't' -> 786
 */
state0_580:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_786;
goto accept0_249;
/*
 * DFA STATE 581
 * ';' -> 787
 */
state0_581:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_787;
goto accept0_249;
/*
 * DFA STATE 582
 * 'l' -> 788
 */
state0_582:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_788;
goto accept0_249;
/*
 * DFA STATE 583
 * '1' -> 789
 * '3' -> 790
 */
state0_583:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x32) /* ('1') '2' */  {
    if(current < 0x31) /* ('0') '1' */ 
        goto accept0_249;
    goto state0_789;
}
if(current < 0x33) /* ('2') '3' */ 
    goto accept0_249;
if(current < 0x34) /* ('3') '4' */ 
    goto state0_790;
goto accept0_249;
/*
 * DFA STATE 584
 * 'a' -> 791
 */
state0_584:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_791;
goto accept0_249;
/*
 * DFA STATE 585
 * ';' -> 792
 */
state0_585:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_792;
goto accept0_249;
/*
 * DFA STATE 586
 * 't' -> 793
 */
state0_586:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_793;
goto accept0_249;
/*
 * DFA STATE 587
 * 'i' -> 794
 */
state0_587:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_794;
goto accept0_249;
/*
 * DFA STATE 588
 * 't' -> 795
 */
state0_588:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_795;
goto accept0_249;
/*
 * DFA STATE 589
 * 'c' -> 796
 */
state0_589:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_796;
goto accept0_249;
/*
 * DFA STATE 590
 * 'l' -> 797
 */
state0_590:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_797;
goto accept0_249;
/*
 * DFA STATE 591
 * 'v' -> 798
 */
state0_591:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_798;
goto accept0_249;
/*
 * DFA STATE 592
 * 'n' -> 799
 */
state0_592:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_799;
goto accept0_249;
/*
 * DFA STATE 593 (accepts to 181)
 */
state0_593:
pEnd = pNext;
goto accept0_181;
/*
 * DFA STATE 594
 * ';' -> 800
 */
state0_594:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_800;
goto accept0_249;
/*
 * DFA STATE 595
 * 's' -> 801
 */
state0_595:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_801;
goto accept0_249;
/*
 * DFA STATE 596
 * ';' -> 802
 */
state0_596:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_802;
goto accept0_249;
/*
 * DFA STATE 597
 * ';' -> 803
 */
state0_597:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_803;
goto accept0_249;
/*
 * DFA STATE 598
 * 'a' -> 804
 */
state0_598:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_804;
goto accept0_249;
/*
 * DFA STATE 599
 * 'd' -> 805
 */
state0_599:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_805;
goto accept0_249;
/*
 * DFA STATE 600
 * 'o' -> 806
 */
state0_600:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_806;
goto accept0_249;
/*
 * DFA STATE 601
 * ';' -> 807
 */
state0_601:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_807;
goto accept0_249;
/*
 * DFA STATE 602
 * 'l' -> 808
 */
state0_602:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_808;
goto accept0_249;
/*
 * DFA STATE 603
 * 'o' -> 809
 */
state0_603:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_809;
goto accept0_249;
/*
 * DFA STATE 604
 * 'o' -> 810
 */
state0_604:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_810;
goto accept0_249;
/*
 * DFA STATE 605
 * 's' -> 811
 */
state0_605:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_811;
goto accept0_249;
/*
 * DFA STATE 606 (accepts to 244)
 */
state0_606:
pEnd = pNext;
goto accept0_244;
/*
 * DFA STATE 607 (accepts to 212)
 */
state0_607:
pEnd = pNext;
goto accept0_212;
/*
 * DFA STATE 608
 * 'u' -> 812
 */
state0_608:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_812;
goto accept0_249;
/*
 * DFA STATE 609
 * 'o' -> 813
 */
state0_609:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_813;
goto accept0_249;
/*
 * DFA STATE 610
 * ';' -> 814
 */
state0_610:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_814;
goto accept0_249;
/*
 * DFA STATE 611
 * 'h' -> 815
 */
state0_611:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_815;
goto accept0_249;
/*
 * DFA STATE 612
 * 'o' -> 816
 */
state0_612:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_816;
goto accept0_249;
/*
 * DFA STATE 613
 * 'o' -> 817
 */
state0_613:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_817;
goto accept0_249;
/*
 * DFA STATE 614
 * 's' -> 818
 */
state0_614:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_818;
goto accept0_249;
/*
 * DFA STATE 615
 * 'a' -> 819
 */
state0_615:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_819;
goto accept0_249;
/*
 * DFA STATE 616
 * ';' -> 820
 */
state0_616:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_820;
goto accept0_249;
/*
 * DFA STATE 617
 * 'h' -> 821
 */
state0_617:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_821;
goto accept0_249;
/*
 * DFA STATE 618 (accepts to 87)
 */
state0_618:
pEnd = pNext;
goto accept0_87;
/*
 * DFA STATE 619
 * 'n' -> 822
 */
state0_619:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_822;
goto accept0_249;
/*
 * DFA STATE 620
 * ';' -> 823
 */
state0_620:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_823;
goto accept0_249;
/*
 * DFA STATE 621
 * 'd' -> 824
 */
state0_621:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_824;
goto accept0_249;
/*
 * DFA STATE 622
 * 't' -> 825
 */
state0_622:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_825;
goto accept0_249;
/*
 * DFA STATE 623
 * 'c' -> 826
 */
state0_623:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_826;
goto accept0_249;
/*
 * DFA STATE 624
 * 'g' -> 827
 */
state0_624:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_827;
goto accept0_249;
/*
 * DFA STATE 625
 * 'v' -> 828
 */
state0_625:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_828;
goto accept0_249;
/*
 * DFA STATE 626
 * 'e' -> 829
 */
state0_626:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_829;
goto accept0_249;
/*
 * DFA STATE 627
 * 'a' -> 830
 */
state0_627:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_830;
goto accept0_249;
/*
 * DFA STATE 628
 * 'r' -> 831
 */
state0_628:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_831;
goto accept0_249;
/*
 * DFA STATE 629
 * 's' -> 832
 */
state0_629:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_832;
goto accept0_249;
/*
 * DFA STATE 630
 * ';' -> 833
 */
state0_630:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_833;
goto accept0_249;
/*
 * DFA STATE 631
 * ';' -> 834
 */
state0_631:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_834;
goto accept0_249;
/*
 * DFA STATE 632
 * 's' -> 835
 */
state0_632:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_835;
goto accept0_249;
/*
 * DFA STATE 633
 * 'd' -> 836
 */
state0_633:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_836;
goto accept0_249;
/*
 * DFA STATE 634
 * 'e' -> 837
 */
state0_634:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_837;
goto accept0_249;
/*
 * DFA STATE 635
 * ';' -> 838
 */
state0_635:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_838;
goto accept0_249;
/*
 * DFA STATE 636
 * ';' -> 839
 */
state0_636:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_839;
goto accept0_249;
/*
 * DFA STATE 637
 * ';' -> 840
 */
state0_637:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_840;
goto accept0_249;
/*
 * DFA STATE 638
 * 'i' -> 841
 */
state0_638:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */ 
    goto accept0_249;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state0_841;
goto accept0_249;
/*
 * DFA STATE 639
 * ';' -> 842
 */
state0_639:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_842;
goto accept0_249;
/*
 * DFA STATE 640 (accepts to 154)
 */
state0_640:
pEnd = pNext;
goto accept0_154;
/*
 * DFA STATE 641 (accepts to 160)
 */
state0_641:
pEnd = pNext;
goto accept0_160;
/*
 * DFA STATE 642
 * 'm' -> 843
 */
state0_642:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_843;
goto accept0_249;
/*
 * DFA STATE 643
 * 'd' -> 844
 */
state0_643:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x64) /* ('c') 'd' */ 
    goto accept0_249;
if(current < 0x65) /* ('d') 'e' */ 
    goto state0_844;
goto accept0_249;
/*
 * DFA STATE 644
 * 'e' -> 845
 */
state0_644:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_845;
goto accept0_249;
/*
 * DFA STATE 645
 * ';' -> 846
 */
state0_645:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_846;
goto accept0_249;
/*
 * DFA STATE 646
 * ';' -> 847
 */
state0_646:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_847;
goto accept0_249;
/*
 * DFA STATE 647 (accepts to 156)
 */
state0_647:
pEnd = pNext;
goto accept0_156;
/*
 * DFA STATE 648
 * ';' -> 848
 */
state0_648:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_848;
goto accept0_249;
/*
 * DFA STATE 649
 * 'c' -> 849
 */
state0_649:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_849;
goto accept0_249;
/*
 * DFA STATE 650
 * 'o' -> 850
 */
state0_650:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_850;
goto accept0_249;
/*
 * DFA STATE 651
 * ';' -> 851
 */
state0_651:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_851;
goto accept0_249;
/*
 * DFA STATE 652
 * 'l' -> 852
 */
state0_652:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_852;
goto accept0_249;
/*
 * DFA STATE 653
 * 'o' -> 853
 */
state0_653:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_853;
goto accept0_249;
/*
 * DFA STATE 654 (accepts to 89)
 */
state0_654:
pEnd = pNext;
goto accept0_89;
/*
 * DFA STATE 655
 * 'o' -> 854
 */
state0_655:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_854;
goto accept0_249;
/*
 * DFA STATE 656 (accepts to 149)
 */
state0_656:
pEnd = pNext;
goto accept0_149;
/*
 * DFA STATE 657 (accepts to 213)
 */
state0_657:
pEnd = pNext;
goto accept0_213;
/*
 * DFA STATE 658
 * 'u' -> 855
 */
state0_658:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x75) /* ('t') 'u' */ 
    goto accept0_249;
if(current < 0x76) /* ('u') 'v' */ 
    goto state0_855;
goto accept0_249;
/*
 * DFA STATE 659
 * 'o' -> 856
 */
state0_659:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_856;
goto accept0_249;
/*
 * DFA STATE 660
 * 'o' -> 857
 */
state0_660:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_857;
goto accept0_249;
/*
 * DFA STATE 661
 * 'o' -> 858
 */
state0_661:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_858;
goto accept0_249;
/*
 * DFA STATE 662
 * ';' -> 859
 */
state0_662:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_859;
goto accept0_249;
/*
 * DFA STATE 663
 * ';' -> 860
 */
state0_663:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_860;
goto accept0_249;
/*
 * DFA STATE 664 (accepts to 88)
 */
state0_664:
pEnd = pNext;
goto accept0_88;
/*
 * DFA STATE 665
 * 'a' -> 861
 */
state0_665:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_861;
goto accept0_249;
/*
 * DFA STATE 666 (accepts to 183)
 */
state0_666:
pEnd = pNext;
goto accept0_183;
/*
 * DFA STATE 667
 * 'e' -> 862
 */
state0_667:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_862;
goto accept0_249;
/*
 * DFA STATE 668 (accepts to 190)
 */
state0_668:
pEnd = pNext;
goto accept0_190;
/*
 * DFA STATE 669
 * ';' -> 863
 */
state0_669:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_863;
goto accept0_249;
/*
 * DFA STATE 670 (accepts to 170)
 */
state0_670:
pEnd = pNext;
goto accept0_170;
/*
 * DFA STATE 671
 * ';' -> 864
 */
state0_671:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_864;
goto accept0_249;
/*
 * DFA STATE 672
 * ';' -> 865
 */
state0_672:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_865;
goto accept0_249;
/*
 * DFA STATE 673
 * ';' -> 866
 */
state0_673:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_866;
goto accept0_249;
/*
 * DFA STATE 674 (accepts to 191)
 */
state0_674:
pEnd = pNext;
goto accept0_191;
/*
 * DFA STATE 675
 * ';' -> 867
 */
state0_675:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_867;
goto accept0_249;
/*
 * DFA STATE 676
 * 'g' -> 868
 */
state0_676:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x67) /* ('f') 'g' */ 
    goto accept0_249;
if(current < 0x68) /* ('g') 'h' */ 
    goto state0_868;
goto accept0_249;
/*
 * DFA STATE 677 (accepts to 152)
 */
state0_677:
pEnd = pNext;
goto accept0_152;
/*
 * DFA STATE 678
 * 'e' -> 869
 */
state0_678:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_869;
goto accept0_249;
/*
 * DFA STATE 679
 * 'a' -> 870
 */
state0_679:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_870;
goto accept0_249;
/*
 * DFA STATE 680
 * 's' -> 871
 */
state0_680:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_871;
goto accept0_249;
/*
 * DFA STATE 681
 * 'n' -> 872
 */
state0_681:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_872;
goto accept0_249;
/*
 * DFA STATE 682
 * 'e' -> 873
 */
state0_682:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_873;
goto accept0_249;
/*
 * DFA STATE 683
 * 's' -> 874
 */
state0_683:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_874;
goto accept0_249;
/*
 * DFA STATE 684
 * 'e' -> 875
 */
state0_684:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_875;
goto accept0_249;
/*
 * DFA STATE 685
 * 't' -> 876
 */
state0_685:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_876;
goto accept0_249;
/*
 * DFA STATE 686
 * ';' -> 877
 */
state0_686:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_877;
goto accept0_249;
/*
 * DFA STATE 687
 * 'c' -> 878
 */
state0_687:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x63) /* ('b') 'c' */ 
    goto accept0_249;
if(current < 0x64) /* ('c') 'd' */ 
    goto state0_878;
goto accept0_249;
/*
 * DFA STATE 688
 * 'v' -> 879
 */
state0_688:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x76) /* ('u') 'v' */ 
    goto accept0_249;
if(current < 0x77) /* ('v') 'w' */ 
    goto state0_879;
goto accept0_249;
/*
 * DFA STATE 689 (accepts to 83)
 */
state0_689:
pEnd = pNext;
goto accept0_83;
/*
 * DFA STATE 690
 * 'h' -> 880
 * 'l' -> 881
 */
state0_690:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x69) /* ('h') 'i' */  {
    if(current < 0x68) /* ('g') 'h' */ 
        goto accept0_249;
    goto state0_880;
}
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_881;
goto accept0_249;
/*
 * DFA STATE 691
 * ';' -> 882
 */
state0_691:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_882;
goto accept0_249;
/*
 * DFA STATE 692
 * 't' -> 883
 */
state0_692:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_883;
goto accept0_249;
/*
 * DFA STATE 693 (accepts to 80)
 */
state0_693:
pEnd = pNext;
goto accept0_80;
/*
 * DFA STATE 694
 * ';' -> 884
 */
state0_694:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_884;
goto accept0_249;
/*
 * DFA STATE 695
 * ';' -> 885
 */
state0_695:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_885;
goto accept0_249;
/*
 * DFA STATE 696 (accepts to 211)
 */
state0_696:
pEnd = pNext;
goto accept0_211;
/*
 * DFA STATE 697
 * ';' -> 886
 */
state0_697:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_886;
goto accept0_249;
/*
 * DFA STATE 698
 * 'A' -> 887
 */
state0_698:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */ 
    goto accept0_3;
if(current < 0x42) /* ('A') 'B' */ 
    goto state0_887;
goto accept0_3;
/*
 * DFA STATE 699 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'O'] -> 247
 * 'P' -> 888
 * ['Q'-'Z'] -> 247
 * ['a'-'o'] -> 247
 * 'p' -> 888
 * ['q'-'z'] -> 247
 */
state0_699:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x50) /* ('O') 'P' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_247;
        goto accept0_6;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto state0_247;
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x51) /* ('P') 'Q' */ 
        goto state0_888;
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_247;
    goto accept0_6;
}
if(current < 0x71) /* ('p') 'q' */  {
    if(current < 0x70) /* ('o') 'p' */ 
        goto state0_247;
    goto state0_888;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 700
 * ';' -> 889
 */
state0_700:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_889;
goto accept0_249;
/*
 * DFA STATE 701
 * 'e' -> 890
 */
state0_701:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_890;
goto accept0_249;
/*
 * DFA STATE 702
 * ';' -> 891
 */
state0_702:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_891;
goto accept0_249;
/*
 * DFA STATE 703
 * 'e' -> 892
 */
state0_703:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_892;
goto accept0_249;
/*
 * DFA STATE 704
 * ';' -> 893
 */
state0_704:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_893;
goto accept0_249;
/*
 * DFA STATE 705
 * ';' -> 894
 */
state0_705:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_894;
goto accept0_249;
/*
 * DFA STATE 706
 * 'e' -> 895
 */
state0_706:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_895;
goto accept0_249;
/*
 * DFA STATE 707 (accepts to 17)
 */
state0_707:
pEnd = pNext;
goto accept0_17;
/*
 * DFA STATE 708 (accepts to 110)
 */
state0_708:
pEnd = pNext;
goto accept0_110;
/*
 * DFA STATE 709
 * 'l' -> 896
 */
state0_709:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_896;
goto accept0_249;
/*
 * DFA STATE 710
 * 'r' -> 897
 */
state0_710:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_897;
goto accept0_249;
/*
 * DFA STATE 711
 * ';' -> 898
 */
state0_711:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_898;
goto accept0_249;
/*
 * DFA STATE 712
 * 'e' -> 899
 */
state0_712:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_899;
goto accept0_249;
/*
 * DFA STATE 713
 * ';' -> 900
 */
state0_713:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_900;
goto accept0_249;
/*
 * DFA STATE 714
 * 'e' -> 901
 */
state0_714:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_901;
goto accept0_249;
/*
 * DFA STATE 715
 * 'o' -> 902
 */
state0_715:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_902;
goto accept0_249;
/*
 * DFA STATE 716 (accepts to 24)
 */
state0_716:
pEnd = pNext;
goto accept0_24;
/*
 * DFA STATE 717
 * ';' -> 903
 */
state0_717:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_903;
goto accept0_249;
/*
 * DFA STATE 718
 * 'e' -> 904
 */
state0_718:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_904;
goto accept0_249;
/*
 * DFA STATE 719
 * ';' -> 905
 */
state0_719:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_905;
goto accept0_249;
/*
 * DFA STATE 720
 * 'e' -> 906
 */
state0_720:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_906;
goto accept0_249;
/*
 * DFA STATE 721 (accepts to 117)
 */
state0_721:
pEnd = pNext;
goto accept0_117;
/*
 * DFA STATE 722 (accepts to 28)
 */
state0_722:
pEnd = pNext;
goto accept0_28;
/*
 * DFA STATE 723
 * ';' -> 907
 */
state0_723:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_907;
goto accept0_249;
/*
 * DFA STATE 724
 * 'a' -> 908
 */
state0_724:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_908;
goto accept0_249;
/*
 * DFA STATE 725
 * 'e' -> 909
 */
state0_725:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_909;
goto accept0_249;
/*
 * DFA STATE 726
 * ';' -> 910
 */
state0_726:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_910;
goto accept0_249;
/*
 * DFA STATE 727
 * 'e' -> 911
 */
state0_727:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_911;
goto accept0_249;
/*
 * DFA STATE 728
 * ';' -> 912
 */
state0_728:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_912;
goto accept0_249;
/*
 * DFA STATE 729
 * 'e' -> 913
 */
state0_729:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_913;
goto accept0_249;
/*
 * DFA STATE 730
 * ';' -> 914
 */
state0_730:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_914;
goto accept0_249;
/*
 * DFA STATE 731
 * 'o' -> 915
 */
state0_731:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_915;
goto accept0_249;
/*
 * DFA STATE 732
 * 'h' -> 916
 */
state0_732:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_916;
goto accept0_249;
/*
 * DFA STATE 733
 * 'e' -> 917
 */
state0_733:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_917;
goto accept0_249;
/*
 * DFA STATE 734 (accepts to 35)
 */
state0_734:
pEnd = pNext;
goto accept0_35;
/*
 * DFA STATE 735
 * ';' -> 918
 */
state0_735:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_918;
goto accept0_249;
/*
 * DFA STATE 736
 * 'n' -> 919
 */
state0_736:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_919;
goto accept0_249;
/*
 * DFA STATE 737
 * ';' -> 920
 */
state0_737:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_920;
goto accept0_249;
/*
 * DFA STATE 738
 * ';' -> 921
 */
state0_738:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_921;
goto accept0_249;
/*
 * DFA STATE 739
 * ';' -> 922
 */
state0_739:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_922;
goto accept0_249;
/*
 * DFA STATE 740
 * 'e' -> 923
 */
state0_740:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_923;
goto accept0_249;
/*
 * DFA STATE 741
 * ';' -> 924
 */
state0_741:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_924;
goto accept0_249;
/*
 * DFA STATE 742
 * 'e' -> 925
 */
state0_742:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_925;
goto accept0_249;
/*
 * DFA STATE 743
 * 'o' -> 926
 */
state0_743:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_926;
goto accept0_249;
/*
 * DFA STATE 744 (accepts to 40)
 */
state0_744:
pEnd = pNext;
goto accept0_40;
/*
 * DFA STATE 745
 * 'e' -> 927
 */
state0_745:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_927;
goto accept0_249;
/*
 * DFA STATE 746 (accepts to 203)
 */
state0_746:
pEnd = pNext;
goto accept0_203;
/*
 * DFA STATE 747 (accepts to 114)
 */
state0_747:
pEnd = pNext;
goto accept0_114;
/*
 * DFA STATE 748
 * 'e' -> 928
 */
state0_748:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_928;
goto accept0_249;
/*
 * DFA STATE 749
 * ';' -> 929
 */
state0_749:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_929;
goto accept0_249;
/*
 * DFA STATE 750
 * ';' -> 930
 */
state0_750:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_930;
goto accept0_249;
/*
 * DFA STATE 751
 * ';' -> 931
 */
state0_751:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_931;
goto accept0_249;
/*
 * DFA STATE 752
 * 'e' -> 932
 */
state0_752:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_932;
goto accept0_249;
/*
 * DFA STATE 753
 * ';' -> 933
 */
state0_753:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_933;
goto accept0_249;
/*
 * DFA STATE 754
 * ';' -> 934
 */
state0_754:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_934;
goto accept0_249;
/*
 * DFA STATE 755
 * ';' -> 935
 */
state0_755:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_935;
goto accept0_249;
/*
 * DFA STATE 756
 * 'e' -> 936
 */
state0_756:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_936;
goto accept0_249;
/*
 * DFA STATE 757 (accepts to 48)
 */
state0_757:
pEnd = pNext;
goto accept0_48;
/*
 * DFA STATE 758
 * ';' -> 937
 */
state0_758:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_937;
goto accept0_249;
/*
 * DFA STATE 759 (accepts to 134)
 */
state0_759:
pEnd = pNext;
goto accept0_134;
/*
 * DFA STATE 760
 * 'r' -> 938
 */
state0_760:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_938;
goto accept0_249;
/*
 * DFA STATE 761 (accepts to 224)
 */
state0_761:
pEnd = pNext;
goto accept0_224;
/*
 * DFA STATE 762
 * 'l' -> 939
 */
state0_762:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_939;
goto accept0_249;
/*
 * DFA STATE 763
 * ';' -> 940
 */
state0_763:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_940;
goto accept0_249;
/*
 * DFA STATE 764 (accepts to 77)
 */
state0_764:
pEnd = pNext;
goto accept0_77;
/*
 * DFA STATE 765 (accepts to 205)
 */
state0_765:
pEnd = pNext;
goto accept0_205;
/*
 * DFA STATE 766
 * ';' -> 941
 */
state0_766:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_941;
goto accept0_249;
/*
 * DFA STATE 767 (accepts to 184)
 */
state0_767:
pEnd = pNext;
goto accept0_184;
/*
 * DFA STATE 768 (accepts to 84)
 */
state0_768:
pEnd = pNext;
goto accept0_84;
/*
 * DFA STATE 769
 * ';' -> 942
 */
state0_769:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_942;
goto accept0_249;
/*
 * DFA STATE 770
 * 'n' -> 943
 */
state0_770:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_943;
goto accept0_249;
/*
 * DFA STATE 771
 * 'r' -> 944
 */
state0_771:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_944;
goto accept0_249;
/*
 * DFA STATE 772 (accepts to 237)
 */
state0_772:
pEnd = pNext;
goto accept0_237;
/*
 * DFA STATE 773
 * ';' -> 945
 */
state0_773:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_945;
goto accept0_249;
/*
 * DFA STATE 774
 * ';' -> 946
 */
state0_774:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_946;
goto accept0_249;
/*
 * DFA STATE 775
 * 'e' -> 947
 */
state0_775:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_947;
goto accept0_249;
/*
 * DFA STATE 776
 * 'e' -> 948
 */
state0_776:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_948;
goto accept0_249;
/*
 * DFA STATE 777
 * ';' -> 949
 */
state0_777:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_949;
goto accept0_249;
/*
 * DFA STATE 778
 * 'e' -> 950
 */
state0_778:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_950;
goto accept0_249;
/*
 * DFA STATE 779
 * ';' -> 951
 */
state0_779:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_951;
goto accept0_249;
/*
 * DFA STATE 780 (accepts to 208)
 */
state0_780:
pEnd = pNext;
goto accept0_208;
/*
 * DFA STATE 781 (accepts to 207)
 */
state0_781:
pEnd = pNext;
goto accept0_207;
/*
 * DFA STATE 782
 * 'o' -> 952
 */
state0_782:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_952;
goto accept0_249;
/*
 * DFA STATE 783
 * ';' -> 953
 */
state0_783:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_953;
goto accept0_249;
/*
 * DFA STATE 784 (accepts to 55)
 */
state0_784:
pEnd = pNext;
goto accept0_55;
/*
 * DFA STATE 785 (accepts to 232)
 */
state0_785:
pEnd = pNext;
goto accept0_232;
/*
 * DFA STATE 786
 * ';' -> 954
 */
state0_786:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_954;
goto accept0_249;
/*
 * DFA STATE 787 (accepts to 204)
 */
state0_787:
pEnd = pNext;
goto accept0_204;
/*
 * DFA STATE 788
 * 'l' -> 955
 */
state0_788:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_955;
goto accept0_249;
/*
 * DFA STATE 789
 * '2' -> 956
 * '4' -> 957
 */
state0_789:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x33) /* ('2') '3' */  {
    if(current < 0x32) /* ('1') '2' */ 
        goto accept0_249;
    goto state0_956;
}
if(current < 0x34) /* ('3') '4' */ 
    goto accept0_249;
if(current < 0x35) /* ('4') '5' */ 
    goto state0_957;
goto accept0_249;
/*
 * DFA STATE 790
 * '4' -> 958
 */
state0_790:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x34) /* ('3') '4' */ 
    goto accept0_249;
if(current < 0x35) /* ('4') '5' */ 
    goto state0_958;
goto accept0_249;
/*
 * DFA STATE 791
 * ';' -> 959
 */
state0_791:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_959;
goto accept0_249;
/*
 * DFA STATE 792 (accepts to 238)
 */
state0_792:
pEnd = pNext;
goto accept0_238;
/*
 * DFA STATE 793
 * 's' -> 960
 */
state0_793:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_960;
goto accept0_249;
/*
 * DFA STATE 794
 * 'p' -> 961
 */
state0_794:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_961;
goto accept0_249;
/*
 * DFA STATE 795
 * 'e' -> 962
 */
state0_795:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_962;
goto accept0_249;
/*
 * DFA STATE 796
 * ';' -> 963
 */
state0_796:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_963;
goto accept0_249;
/*
 * DFA STATE 797
 * ';' -> 964
 */
state0_797:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_964;
goto accept0_249;
/*
 * DFA STATE 798
 * 'e' -> 965
 */
state0_798:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_965;
goto accept0_249;
/*
 * DFA STATE 799
 * ';' -> 966
 */
state0_799:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_966;
goto accept0_249;
/*
 * DFA STATE 800 (accepts to 141)
 */
state0_800:
pEnd = pNext;
goto accept0_141;
/*
 * DFA STATE 801
 * 't' -> 967
 */
state0_801:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_967;
goto accept0_249;
/*
 * DFA STATE 802 (accepts to 166)
 */
state0_802:
pEnd = pNext;
goto accept0_166;
/*
 * DFA STATE 803 (accepts to 59)
 */
state0_803:
pEnd = pNext;
goto accept0_59;
/*
 * DFA STATE 804
 * ';' -> 968
 */
state0_804:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_968;
goto accept0_249;
/*
 * DFA STATE 805
 * 'a' -> 969
 */
state0_805:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept0_249;
if(current < 0x62) /* ('a') 'b' */ 
    goto state0_969;
goto accept0_249;
/*
 * DFA STATE 806
 * ';' -> 970
 */
state0_806:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_970;
goto accept0_249;
/*
 * DFA STATE 807 (accepts to 234)
 */
state0_807:
pEnd = pNext;
goto accept0_234;
/*
 * DFA STATE 808
 * ';' -> 971
 */
state0_808:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_971;
goto accept0_249;
/*
 * DFA STATE 809
 * ';' -> 972
 */
state0_809:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_972;
goto accept0_249;
/*
 * DFA STATE 810
 * 'r' -> 973
 */
state0_810:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_973;
goto accept0_249;
/*
 * DFA STATE 811
 * 't' -> 974
 */
state0_811:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_974;
goto accept0_249;
/*
 * DFA STATE 812
 * 'o' -> 975
 */
state0_812:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_975;
goto accept0_249;
/*
 * DFA STATE 813
 * ';' -> 976
 */
state0_813:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_976;
goto accept0_249;
/*
 * DFA STATE 814 (accepts to 90)
 */
state0_814:
pEnd = pNext;
goto accept0_90;
/*
 * DFA STATE 815
 * ';' -> 977
 */
state0_815:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_977;
goto accept0_249;
/*
 * DFA STATE 816
 * ';' -> 978
 */
state0_816:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_978;
goto accept0_249;
/*
 * DFA STATE 817
 * 't' -> 979
 */
state0_817:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x74) /* ('s') 't' */ 
    goto accept0_249;
if(current < 0x75) /* ('t') 'u' */ 
    goto state0_979;
goto accept0_249;
/*
 * DFA STATE 818
 * ';' -> 980
 */
state0_818:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_980;
goto accept0_249;
/*
 * DFA STATE 819
 * ';' -> 981
 */
state0_819:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_981;
goto accept0_249;
/*
 * DFA STATE 820 (accepts to 75)
 */
state0_820:
pEnd = pNext;
goto accept0_75;
/*
 * DFA STATE 821
 * ';' -> 982
 */
state0_821:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_982;
goto accept0_249;
/*
 * DFA STATE 822
 * ';' -> 983
 */
state0_822:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_983;
goto accept0_249;
/*
 * DFA STATE 823 (accepts to 192)
 */
state0_823:
pEnd = pNext;
goto accept0_192;
/*
 * DFA STATE 824
 * 'e' -> 984
 */
state0_824:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_984;
goto accept0_249;
/*
 * DFA STATE 825
 * 'e' -> 985
 */
state0_825:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_985;
goto accept0_249;
/*
 * DFA STATE 826
 * ';' -> 986
 */
state0_826:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_986;
goto accept0_249;
/*
 * DFA STATE 827
 * ';' -> 987
 */
state0_827:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_987;
goto accept0_249;
/*
 * DFA STATE 828
 * 'e' -> 988
 */
state0_828:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_988;
goto accept0_249;
/*
 * DFA STATE 829
 * ';' -> 989
 */
state0_829:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_989;
goto accept0_249;
/*
 * DFA STATE 830
 * ';' -> 990
 */
state0_830:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_990;
goto accept0_249;
/*
 * DFA STATE 831
 * 'o' -> 991
 */
state0_831:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_991;
goto accept0_249;
/*
 * DFA STATE 832
 * ';' -> 992
 */
state0_832:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_992;
goto accept0_249;
/*
 * DFA STATE 833 (accepts to 85)
 */
state0_833:
pEnd = pNext;
goto accept0_85;
/*
 * DFA STATE 834 (accepts to 101)
 */
state0_834:
pEnd = pNext;
goto accept0_101;
/*
 * DFA STATE 835
 * 'h' -> 993
 */
state0_835:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x68) /* ('g') 'h' */ 
    goto accept0_249;
if(current < 0x69) /* ('h') 'i' */ 
    goto state0_993;
goto accept0_249;
/*
 * DFA STATE 836
 * 'e' -> 994
 */
state0_836:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_994;
goto accept0_249;
/*
 * DFA STATE 837
 * 's' -> 995
 */
state0_837:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_995;
goto accept0_249;
/*
 * DFA STATE 838 (accepts to 66)
 */
state0_838:
pEnd = pNext;
goto accept0_66;
/*
 * DFA STATE 839 (accepts to 97)
 */
state0_839:
pEnd = pNext;
goto accept0_97;
/*
 * DFA STATE 840 (accepts to 162)
 */
state0_840:
pEnd = pNext;
goto accept0_162;
/*
 * DFA STATE 841
 * 'l' -> 996
 */
state0_841:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept0_249;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state0_996;
goto accept0_249;
/*
 * DFA STATE 842 (accepts to 197)
 */
state0_842:
pEnd = pNext;
goto accept0_197;
/*
 * DFA STATE 843
 * 'n' -> 997
 */
state0_843:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_997;
goto accept0_249;
/*
 * DFA STATE 844
 * ';' -> 998
 */
state0_844:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_998;
goto accept0_249;
/*
 * DFA STATE 845
 * ';' -> 999
 */
state0_845:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_999;
goto accept0_249;
/*
 * DFA STATE 846 (accepts to 169)
 */
state0_846:
pEnd = pNext;
goto accept0_169;
/*
 * DFA STATE 847 (accepts to 174)
 */
state0_847:
pEnd = pNext;
goto accept0_174;
/*
 * DFA STATE 848 (accepts to 9)
 */
state0_848:
pEnd = pNext;
goto accept0_9;
/*
 * DFA STATE 849
 * ';' -> 1000
 */
state0_849:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1000;
goto accept0_249;
/*
 * DFA STATE 850
 * ';' -> 1001
 */
state0_850:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1001;
goto accept0_249;
/*
 * DFA STATE 851 (accepts to 236)
 */
state0_851:
pEnd = pNext;
goto accept0_236;
/*
 * DFA STATE 852
 * ';' -> 1002
 */
state0_852:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1002;
goto accept0_249;
/*
 * DFA STATE 853
 * ';' -> 1003
 */
state0_853:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1003;
goto accept0_249;
/*
 * DFA STATE 854
 * 'r' -> 1004
 */
state0_854:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */ 
    goto accept0_249;
if(current < 0x73) /* ('r') 's' */ 
    goto state0_1004;
goto accept0_249;
/*
 * DFA STATE 855
 * 'o' -> 1005
 */
state0_855:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_1005;
goto accept0_249;
/*
 * DFA STATE 856
 * ';' -> 1006
 */
state0_856:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1006;
goto accept0_249;
/*
 * DFA STATE 857
 * ';' -> 1007
 */
state0_857:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1007;
goto accept0_249;
/*
 * DFA STATE 858
 * 'n' -> 1008
 */
state0_858:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1008;
goto accept0_249;
/*
 * DFA STATE 859 (accepts to 198)
 */
state0_859:
pEnd = pNext;
goto accept0_198;
/*
 * DFA STATE 860 (accepts to 82)
 */
state0_860:
pEnd = pNext;
goto accept0_82;
/*
 * DFA STATE 861
 * ';' -> 1009
 * 'f' -> 1010
 */
state0_861:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_1009;
}
if(current < 0x66) /* ('e') 'f' */ 
    goto accept0_249;
if(current < 0x67) /* ('f') 'g' */ 
    goto state0_1010;
goto accept0_249;
/*
 * DFA STATE 862
 * 's' -> 1011
 */
state0_862:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_1011;
goto accept0_249;
/*
 * DFA STATE 863 (accepts to 193)
 */
state0_863:
pEnd = pNext;
goto accept0_193;
/*
 * DFA STATE 864 (accepts to 100)
 */
state0_864:
pEnd = pNext;
goto accept0_100;
/*
 * DFA STATE 865 (accepts to 93)
 */
state0_865:
pEnd = pNext;
goto accept0_93;
/*
 * DFA STATE 866 (accepts to 94)
 */
state0_866:
pEnd = pNext;
goto accept0_94;
/*
 * DFA STATE 867 (accepts to 194)
 */
state0_867:
pEnd = pNext;
goto accept0_194;
/*
 * DFA STATE 868
 * ';' -> 1012
 */
state0_868:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1012;
goto accept0_249;
/*
 * DFA STATE 869
 * '4' -> 1013
 */
state0_869:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x34) /* ('3') '4' */ 
    goto accept0_249;
if(current < 0x35) /* ('4') '5' */ 
    goto state0_1013;
goto accept0_249;
/*
 * DFA STATE 870
 * ';' -> 1014
 * 's' -> 1015
 */
state0_870:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3C) /* (';') '<' */  {
    if(current < 0x3B) /* (':') ';' */ 
        goto accept0_249;
    goto state0_1014;
}
if(current < 0x73) /* ('r') 's' */ 
    goto accept0_249;
if(current < 0x74) /* ('s') 't' */ 
    goto state0_1015;
goto accept0_249;
/*
 * DFA STATE 871
 * 'p' -> 1016
 */
state0_871:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x70) /* ('o') 'p' */ 
    goto accept0_249;
if(current < 0x71) /* ('p') 'q' */ 
    goto state0_1016;
goto accept0_249;
/*
 * DFA STATE 872
 * ';' -> 1017
 */
state0_872:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1017;
goto accept0_249;
/*
 * DFA STATE 873
 * ';' -> 1018
 */
state0_873:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1018;
goto accept0_249;
/*
 * DFA STATE 874
 * ';' -> 1019
 */
state0_874:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1019;
goto accept0_249;
/*
 * DFA STATE 875
 * ';' -> 1020
 */
state0_875:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1020;
goto accept0_249;
/*
 * DFA STATE 876
 * 'e' -> 1021
 */
state0_876:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_1021;
goto accept0_249;
/*
 * DFA STATE 877 (accepts to 235)
 */
state0_877:
pEnd = pNext;
goto accept0_235;
/*
 * DFA STATE 878
 * ';' -> 1022
 */
state0_878:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1022;
goto accept0_249;
/*
 * DFA STATE 879
 * 'e' -> 1023
 */
state0_879:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_1023;
goto accept0_249;
/*
 * DFA STATE 880
 * ';' -> 1024
 */
state0_880:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1024;
goto accept0_249;
/*
 * DFA STATE 881
 * 'o' -> 1025
 */
state0_881:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */ 
    goto accept0_249;
if(current < 0x70) /* ('o') 'p' */ 
    goto state0_1025;
goto accept0_249;
/*
 * DFA STATE 882 (accepts to 71)
 */
state0_882:
pEnd = pNext;
goto accept0_71;
/*
 * DFA STATE 883
 * 'e' -> 1026
 */
state0_883:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x65) /* ('d') 'e' */ 
    goto accept0_249;
if(current < 0x66) /* ('e') 'f' */ 
    goto state0_1026;
goto accept0_249;
/*
 * DFA STATE 884 (accepts to 74)
 */
state0_884:
pEnd = pNext;
goto accept0_74;
/*
 * DFA STATE 885 (accepts to 138)
 */
state0_885:
pEnd = pNext;
goto accept0_138;
/*
 * DFA STATE 886 (accepts to 210)
 */
state0_886:
pEnd = pNext;
goto accept0_210;
/*
 * DFA STATE 887
 * 'T' -> 1027
 */
state0_887:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x54) /* ('S') 'T' */ 
    goto accept0_3;
if(current < 0x55) /* ('T') 'U' */ 
    goto state0_1027;
goto accept0_3;
/*
 * DFA STATE 888 (accepts to 6)
 * '-' -> 247
 * ['0'-'9'] -> 247
 * ':' -> 247
 * ['A'-'S'] -> 247
 * 'T' -> 1028
 * ['U'-'Z'] -> 247
 * ['a'-'s'] -> 247
 * 't' -> 1028
 * ['u'-'z'] -> 247
 */
state0_888:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_6;
current = *pNext++;
if(current < 0x54) /* ('S') 'T' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept0_6;
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_247;
        goto accept0_6;
    }
    if(current < 0x3B) /* (':') ';' */ 
        goto state0_247;
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept0_6;
    goto state0_247;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x55) /* ('T') 'U' */ 
        goto state0_1028;
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_247;
    goto accept0_6;
}
if(current < 0x75) /* ('t') 'u' */  {
    if(current < 0x74) /* ('s') 't' */ 
        goto state0_247;
    goto state0_1028;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_247;
goto accept0_6;
/*
 * DFA STATE 889 (accepts to 19)
 */
state0_889:
pEnd = pNext;
goto accept0_19;
/*
 * DFA STATE 890
 * ';' -> 1029
 */
state0_890:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1029;
goto accept0_249;
/*
 * DFA STATE 891 (accepts to 15)
 */
state0_891:
pEnd = pNext;
goto accept0_15;
/*
 * DFA STATE 892
 * ';' -> 1030
 */
state0_892:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1030;
goto accept0_249;
/*
 * DFA STATE 893 (accepts to 109)
 */
state0_893:
pEnd = pNext;
goto accept0_109;
/*
 * DFA STATE 894 (accepts to 18)
 */
state0_894:
pEnd = pNext;
goto accept0_18;
/*
 * DFA STATE 895
 * ';' -> 1031
 */
state0_895:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1031;
goto accept0_249;
/*
 * DFA STATE 896
 * ';' -> 1032
 */
state0_896:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1032;
goto accept0_249;
/*
 * DFA STATE 897
 * ';' -> 1033
 */
state0_897:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1033;
goto accept0_249;
/*
 * DFA STATE 898 (accepts to 112)
 */
state0_898:
pEnd = pNext;
goto accept0_112;
/*
 * DFA STATE 899
 * ';' -> 1034
 */
state0_899:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1034;
goto accept0_249;
/*
 * DFA STATE 900 (accepts to 23)
 */
state0_900:
pEnd = pNext;
goto accept0_23;
/*
 * DFA STATE 901
 * ';' -> 1035
 */
state0_901:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1035;
goto accept0_249;
/*
 * DFA STATE 902
 * 'n' -> 1036
 */
state0_902:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1036;
goto accept0_249;
/*
 * DFA STATE 903 (accepts to 111)
 */
state0_903:
pEnd = pNext;
goto accept0_111;
/*
 * DFA STATE 904
 * ';' -> 1037
 */
state0_904:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1037;
goto accept0_249;
/*
 * DFA STATE 905 (accepts to 27)
 */
state0_905:
pEnd = pNext;
goto accept0_27;
/*
 * DFA STATE 906
 * ';' -> 1038
 */
state0_906:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1038;
goto accept0_249;
/*
 * DFA STATE 907 (accepts to 118)
 */
state0_907:
pEnd = pNext;
goto accept0_118;
/*
 * DFA STATE 908
 * ';' -> 1039
 */
state0_908:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1039;
goto accept0_249;
/*
 * DFA STATE 909
 * ';' -> 1040
 */
state0_909:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1040;
goto accept0_249;
/*
 * DFA STATE 910 (accepts to 199)
 */
state0_910:
pEnd = pNext;
goto accept0_199;
/*
 * DFA STATE 911
 * ';' -> 1041
 */
state0_911:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1041;
goto accept0_249;
/*
 * DFA STATE 912 (accepts to 33)
 */
state0_912:
pEnd = pNext;
goto accept0_33;
/*
 * DFA STATE 913
 * ';' -> 1042
 */
state0_913:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1042;
goto accept0_249;
/*
 * DFA STATE 914 (accepts to 132)
 */
state0_914:
pEnd = pNext;
goto accept0_132;
/*
 * DFA STATE 915
 * 'n' -> 1043
 */
state0_915:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1043;
goto accept0_249;
/*
 * DFA STATE 916
 * ';' -> 1044
 */
state0_916:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1044;
goto accept0_249;
/*
 * DFA STATE 917
 * ';' -> 1045
 */
state0_917:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1045;
goto accept0_249;
/*
 * DFA STATE 918 (accepts to 228)
 */
state0_918:
pEnd = pNext;
goto accept0_228;
/*
 * DFA STATE 919
 * ';' -> 1046
 */
state0_919:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1046;
goto accept0_249;
/*
 * DFA STATE 920 (accepts to 126)
 */
state0_920:
pEnd = pNext;
goto accept0_126;
/*
 * DFA STATE 921 (accepts to 42)
 */
state0_921:
pEnd = pNext;
goto accept0_42;
/*
 * DFA STATE 922 (accepts to 116)
 */
state0_922:
pEnd = pNext;
goto accept0_116;
/*
 * DFA STATE 923
 * ';' -> 1047
 */
state0_923:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1047;
goto accept0_249;
/*
 * DFA STATE 924 (accepts to 39)
 */
state0_924:
pEnd = pNext;
goto accept0_39;
/*
 * DFA STATE 925
 * ';' -> 1048
 */
state0_925:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1048;
goto accept0_249;
/*
 * DFA STATE 926
 * 'n' -> 1049
 */
state0_926:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1049;
goto accept0_249;
/*
 * DFA STATE 927
 * ';' -> 1050
 */
state0_927:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1050;
goto accept0_249;
/*
 * DFA STATE 928
 * ';' -> 1051
 */
state0_928:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1051;
goto accept0_249;
/*
 * DFA STATE 929 (accepts to 46)
 */
state0_929:
pEnd = pNext;
goto accept0_46;
/*
 * DFA STATE 930 (accepts to 95)
 */
state0_930:
pEnd = pNext;
goto accept0_95;
/*
 * DFA STATE 931 (accepts to 50)
 */
state0_931:
pEnd = pNext;
goto accept0_50;
/*
 * DFA STATE 932
 * ';' -> 1052
 */
state0_932:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1052;
goto accept0_249;
/*
 * DFA STATE 933 (accepts to 133)
 */
state0_933:
pEnd = pNext;
goto accept0_133;
/*
 * DFA STATE 934 (accepts to 49)
 */
state0_934:
pEnd = pNext;
goto accept0_49;
/*
 * DFA STATE 935 (accepts to 185)
 */
state0_935:
pEnd = pNext;
goto accept0_185;
/*
 * DFA STATE 936
 * ';' -> 1053
 */
state0_936:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1053;
goto accept0_249;
/*
 * DFA STATE 937 (accepts to 221)
 */
state0_937:
pEnd = pNext;
goto accept0_221;
/*
 * DFA STATE 938
 * ';' -> 1054
 */
state0_938:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1054;
goto accept0_249;
/*
 * DFA STATE 939
 * ';' -> 1055
 */
state0_939:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1055;
goto accept0_249;
/*
 * DFA STATE 940 (accepts to 99)
 */
state0_940:
pEnd = pNext;
goto accept0_99;
/*
 * DFA STATE 941 (accepts to 246)
 */
state0_941:
pEnd = pNext;
goto accept0_246;
/*
 * DFA STATE 942 (accepts to 239)
 */
state0_942:
pEnd = pNext;
goto accept0_239;
/*
 * DFA STATE 943
 * ';' -> 1056
 */
state0_943:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1056;
goto accept0_249;
/*
 * DFA STATE 944
 * ';' -> 1057
 */
state0_944:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1057;
goto accept0_249;
/*
 * DFA STATE 945 (accepts to 136)
 */
state0_945:
pEnd = pNext;
goto accept0_136;
/*
 * DFA STATE 946 (accepts to 248)
 */
state0_946:
pEnd = pNext;
goto accept0_248;
/*
 * DFA STATE 947
 * ';' -> 1058
 */
state0_947:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1058;
goto accept0_249;
/*
 * DFA STATE 948
 * ';' -> 1059
 */
state0_948:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1059;
goto accept0_249;
/*
 * DFA STATE 949 (accepts to 54)
 */
state0_949:
pEnd = pNext;
goto accept0_54;
/*
 * DFA STATE 950
 * ';' -> 1060
 */
state0_950:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1060;
goto accept0_249;
/*
 * DFA STATE 951 (accepts to 164)
 */
state0_951:
pEnd = pNext;
goto accept0_164;
/*
 * DFA STATE 952
 * 'n' -> 1061
 */
state0_952:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1061;
goto accept0_249;
/*
 * DFA STATE 953 (accepts to 187)
 */
state0_953:
pEnd = pNext;
goto accept0_187;
/*
 * DFA STATE 954 (accepts to 163)
 */
state0_954:
pEnd = pNext;
goto accept0_163;
/*
 * DFA STATE 955
 * ';' -> 1062
 */
state0_955:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1062;
goto accept0_249;
/*
 * DFA STATE 956
 * ';' -> 1063
 */
state0_956:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1063;
goto accept0_249;
/*
 * DFA STATE 957
 * ';' -> 1064
 */
state0_957:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1064;
goto accept0_249;
/*
 * DFA STATE 958
 * ';' -> 1065
 */
state0_958:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1065;
goto accept0_249;
/*
 * DFA STATE 959 (accepts to 135)
 */
state0_959:
pEnd = pNext;
goto accept0_135;
/*
 * DFA STATE 960
 * ';' -> 1066
 */
state0_960:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1066;
goto accept0_249;
/*
 * DFA STATE 961
 * ';' -> 1067
 */
state0_961:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1067;
goto accept0_249;
/*
 * DFA STATE 962
 * ';' -> 1068
 */
state0_962:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1068;
goto accept0_249;
/*
 * DFA STATE 963 (accepts to 58)
 */
state0_963:
pEnd = pNext;
goto accept0_58;
/*
 * DFA STATE 964 (accepts to 76)
 */
state0_964:
pEnd = pNext;
goto accept0_76;
/*
 * DFA STATE 965
 * ';' -> 1069
 */
state0_965:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1069;
goto accept0_249;
/*
 * DFA STATE 966 (accepts to 175)
 */
state0_966:
pEnd = pNext;
goto accept0_175;
/*
 * DFA STATE 967
 * ';' -> 1070
 */
state0_967:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1070;
goto accept0_249;
/*
 * DFA STATE 968 (accepts to 142)
 */
state0_968:
pEnd = pNext;
goto accept0_142;
/*
 * DFA STATE 969
 * ';' -> 1071
 */
state0_969:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1071;
goto accept0_249;
/*
 * DFA STATE 970 (accepts to 86)
 */
state0_970:
pEnd = pNext;
goto accept0_86;
/*
 * DFA STATE 971 (accepts to 240)
 */
state0_971:
pEnd = pNext;
goto accept0_240;
/*
 * DFA STATE 972 (accepts to 219)
 */
state0_972:
pEnd = pNext;
goto accept0_219;
/*
 * DFA STATE 973
 * ';' -> 1072
 */
state0_973:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1072;
goto accept0_249;
/*
 * DFA STATE 974
 * ';' -> 1073
 */
state0_974:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1073;
goto accept0_249;
/*
 * DFA STATE 975
 * ';' -> 1074
 */
state0_975:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1074;
goto accept0_249;
/*
 * DFA STATE 976 (accepts to 216)
 */
state0_976:
pEnd = pNext;
goto accept0_216;
/*
 * DFA STATE 977 (accepts to 215)
 */
state0_977:
pEnd = pNext;
goto accept0_215;
/*
 * DFA STATE 978 (accepts to 96)
 */
state0_978:
pEnd = pNext;
goto accept0_96;
/*
 * DFA STATE 979
 * ';' -> 1075
 */
state0_979:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1075;
goto accept0_249;
/*
 * DFA STATE 980 (accepts to 171)
 */
state0_980:
pEnd = pNext;
goto accept0_171;
/*
 * DFA STATE 981 (accepts to 165)
 */
state0_981:
pEnd = pNext;
goto accept0_165;
/*
 * DFA STATE 982 (accepts to 214)
 */
state0_982:
pEnd = pNext;
goto accept0_214;
/*
 * DFA STATE 983 (accepts to 167)
 */
state0_983:
pEnd = pNext;
goto accept0_167;
/*
 * DFA STATE 984
 * ';' -> 1076
 */
state0_984:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1076;
goto accept0_249;
/*
 * DFA STATE 985
 * ';' -> 1077
 */
state0_985:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1077;
goto accept0_249;
/*
 * DFA STATE 986 (accepts to 64)
 */
state0_986:
pEnd = pNext;
goto accept0_64;
/*
 * DFA STATE 987 (accepts to 200)
 */
state0_987:
pEnd = pNext;
goto accept0_200;
/*
 * DFA STATE 988
 * ';' -> 1078
 */
state0_988:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1078;
goto accept0_249;
/*
 * DFA STATE 989 (accepts to 231)
 */
state0_989:
pEnd = pNext;
goto accept0_231;
/*
 * DFA STATE 990 (accepts to 157)
 */
state0_990:
pEnd = pNext;
goto accept0_157;
/*
 * DFA STATE 991
 * 'n' -> 1079
 */
state0_991:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1079;
goto accept0_249;
/*
 * DFA STATE 992 (accepts to 195)
 */
state0_992:
pEnd = pNext;
goto accept0_195;
/*
 * DFA STATE 993
 * ';' -> 1080
 */
state0_993:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1080;
goto accept0_249;
/*
 * DFA STATE 994
 * ';' -> 1081
 */
state0_994:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1081;
goto accept0_249;
/*
 * DFA STATE 995
 * ';' -> 1082
 */
state0_995:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1082;
goto accept0_249;
/*
 * DFA STATE 996
 * ';' -> 1083
 */
state0_996:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1083;
goto accept0_249;
/*
 * DFA STATE 997
 * ';' -> 1084
 */
state0_997:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1084;
goto accept0_249;
/*
 * DFA STATE 998 (accepts to 78)
 */
state0_998:
pEnd = pNext;
goto accept0_78;
/*
 * DFA STATE 999 (accepts to 227)
 */
state0_999:
pEnd = pNext;
goto accept0_227;
/*
 * DFA STATE 1000 (accepts to 173)
 */
state0_1000:
pEnd = pNext;
goto accept0_173;
/*
 * DFA STATE 1001 (accepts to 102)
 */
state0_1001:
pEnd = pNext;
goto accept0_102;
/*
 * DFA STATE 1002 (accepts to 241)
 */
state0_1002:
pEnd = pNext;
goto accept0_241;
/*
 * DFA STATE 1003 (accepts to 220)
 */
state0_1003:
pEnd = pNext;
goto accept0_220;
/*
 * DFA STATE 1004
 * ';' -> 1085
 */
state0_1004:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1085;
goto accept0_249;
/*
 * DFA STATE 1005
 * ';' -> 1086
 */
state0_1005:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1086;
goto accept0_249;
/*
 * DFA STATE 1006 (accepts to 217)
 */
state0_1006:
pEnd = pNext;
goto accept0_217;
/*
 * DFA STATE 1007 (accepts to 218)
 */
state0_1007:
pEnd = pNext;
goto accept0_218;
/*
 * DFA STATE 1008
 * ';' -> 1087
 */
state0_1008:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1087;
goto accept0_249;
/*
 * DFA STATE 1009 (accepts to 151)
 */
state0_1009:
pEnd = pNext;
goto accept0_151;
/*
 * DFA STATE 1010
 * ';' -> 1088
 */
state0_1010:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1088;
goto accept0_249;
/*
 * DFA STATE 1011
 * ';' -> 1089
 */
state0_1011:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1089;
goto accept0_249;
/*
 * DFA STATE 1012 (accepts to 43)
 */
state0_1012:
pEnd = pNext;
goto accept0_43;
/*
 * DFA STATE 1013
 * ';' -> 1090
 */
state0_1013:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1090;
goto accept0_249;
/*
 * DFA STATE 1014 (accepts to 140)
 */
state0_1014:
pEnd = pNext;
goto accept0_140;
/*
 * DFA STATE 1015
 * 'y' -> 1091
 */
state0_1015:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x79) /* ('x') 'y' */ 
    goto accept0_249;
if(current < 0x7A) /* ('y') 'z' */ 
    goto state0_1091;
goto accept0_249;
/*
 * DFA STATE 1016
 * ';' -> 1092
 */
state0_1016:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1092;
goto accept0_249;
/*
 * DFA STATE 1017 (accepts to 73)
 */
state0_1017:
pEnd = pNext;
goto accept0_73;
/*
 * DFA STATE 1018 (accepts to 206)
 */
state0_1018:
pEnd = pNext;
goto accept0_206;
/*
 * DFA STATE 1019 (accepts to 107)
 */
state0_1019:
pEnd = pNext;
goto accept0_107;
/*
 * DFA STATE 1020 (accepts to 233)
 */
state0_1020:
pEnd = pNext;
goto accept0_233;
/*
 * DFA STATE 1021
 * ';' -> 1093
 */
state0_1021:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1093;
goto accept0_249;
/*
 * DFA STATE 1022 (accepts to 70)
 */
state0_1022:
pEnd = pNext;
goto accept0_70;
/*
 * DFA STATE 1023
 * ';' -> 1094
 */
state0_1023:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1094;
goto accept0_249;
/*
 * DFA STATE 1024 (accepts to 159)
 */
state0_1024:
pEnd = pNext;
goto accept0_159;
/*
 * DFA STATE 1025
 * 'n' -> 1095
 */
state0_1025:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept0_249;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state0_1095;
goto accept0_249;
/*
 * DFA STATE 1026
 * ';' -> 1096
 */
state0_1026:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1096;
goto accept0_249;
/*
 * DFA STATE 1027
 * 'A' -> 1097
 */
state0_1027:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */ 
    goto accept0_3;
if(current < 0x42) /* ('A') 'B' */ 
    goto state0_1097;
goto accept0_3;
/*
 * DFA STATE 1028 (accepts to 4)
 */
state0_1028:
pEnd = pNext;
goto accept0_4;
/*
 * DFA STATE 1029 (accepts to 14)
 */
state0_1029:
pEnd = pNext;
goto accept0_14;
/*
 * DFA STATE 1030 (accepts to 13)
 */
state0_1030:
pEnd = pNext;
goto accept0_13;
/*
 * DFA STATE 1031 (accepts to 16)
 */
state0_1031:
pEnd = pNext;
goto accept0_16;
/*
 * DFA STATE 1032 (accepts to 20)
 */
state0_1032:
pEnd = pNext;
goto accept0_20;
/*
 * DFA STATE 1033 (accepts to 223)
 */
state0_1033:
pEnd = pNext;
goto accept0_223;
/*
 * DFA STATE 1034 (accepts to 22)
 */
state0_1034:
pEnd = pNext;
goto accept0_22;
/*
 * DFA STATE 1035 (accepts to 21)
 */
state0_1035:
pEnd = pNext;
goto accept0_21;
/*
 * DFA STATE 1036
 * ';' -> 1098
 */
state0_1036:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1098;
goto accept0_249;
/*
 * DFA STATE 1037 (accepts to 26)
 */
state0_1037:
pEnd = pNext;
goto accept0_26;
/*
 * DFA STATE 1038 (accepts to 25)
 */
state0_1038:
pEnd = pNext;
goto accept0_25;
/*
 * DFA STATE 1039 (accepts to 119)
 */
state0_1039:
pEnd = pNext;
goto accept0_119;
/*
 * DFA STATE 1040 (accepts to 30)
 */
state0_1040:
pEnd = pNext;
goto accept0_30;
/*
 * DFA STATE 1041 (accepts to 32)
 */
state0_1041:
pEnd = pNext;
goto accept0_32;
/*
 * DFA STATE 1042 (accepts to 31)
 */
state0_1042:
pEnd = pNext;
goto accept0_31;
/*
 * DFA STATE 1043
 * ';' -> 1099
 */
state0_1043:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1099;
goto accept0_249;
/*
 * DFA STATE 1044 (accepts to 36)
 */
state0_1044:
pEnd = pNext;
goto accept0_36;
/*
 * DFA STATE 1045 (accepts to 34)
 */
state0_1045:
pEnd = pNext;
goto accept0_34;
/*
 * DFA STATE 1046 (accepts to 201)
 */
state0_1046:
pEnd = pNext;
goto accept0_201;
/*
 * DFA STATE 1047 (accepts to 38)
 */
state0_1047:
pEnd = pNext;
goto accept0_38;
/*
 * DFA STATE 1048 (accepts to 37)
 */
state0_1048:
pEnd = pNext;
goto accept0_37;
/*
 * DFA STATE 1049
 * ';' -> 1100
 */
state0_1049:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1100;
goto accept0_249;
/*
 * DFA STATE 1050 (accepts to 41)
 */
state0_1050:
pEnd = pNext;
goto accept0_41;
/*
 * DFA STATE 1051 (accepts to 45)
 */
state0_1051:
pEnd = pNext;
goto accept0_45;
/*
 * DFA STATE 1052 (accepts to 44)
 */
state0_1052:
pEnd = pNext;
goto accept0_44;
/*
 * DFA STATE 1053 (accepts to 47)
 */
state0_1053:
pEnd = pNext;
goto accept0_47;
/*
 * DFA STATE 1054 (accepts to 81)
 */
state0_1054:
pEnd = pNext;
goto accept0_81;
/*
 * DFA STATE 1055 (accepts to 51)
 */
state0_1055:
pEnd = pNext;
goto accept0_51;
/*
 * DFA STATE 1056 (accepts to 79)
 */
state0_1056:
pEnd = pNext;
goto accept0_79;
/*
 * DFA STATE 1057 (accepts to 222)
 */
state0_1057:
pEnd = pNext;
goto accept0_222;
/*
 * DFA STATE 1058 (accepts to 108)
 */
state0_1058:
pEnd = pNext;
goto accept0_108;
/*
 * DFA STATE 1059 (accepts to 53)
 */
state0_1059:
pEnd = pNext;
goto accept0_53;
/*
 * DFA STATE 1060 (accepts to 52)
 */
state0_1060:
pEnd = pNext;
goto accept0_52;
/*
 * DFA STATE 1061
 * ';' -> 1101
 */
state0_1061:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1101;
goto accept0_249;
/*
 * DFA STATE 1062 (accepts to 161)
 */
state0_1062:
pEnd = pNext;
goto accept0_161;
/*
 * DFA STATE 1063 (accepts to 104)
 */
state0_1063:
pEnd = pNext;
goto accept0_104;
/*
 * DFA STATE 1064 (accepts to 103)
 */
state0_1064:
pEnd = pNext;
goto accept0_103;
/*
 * DFA STATE 1065 (accepts to 105)
 */
state0_1065:
pEnd = pNext;
goto accept0_105;
/*
 * DFA STATE 1066 (accepts to 247)
 */
state0_1066:
pEnd = pNext;
goto accept0_247;
/*
 * DFA STATE 1067 (accepts to 225)
 */
state0_1067:
pEnd = pNext;
goto accept0_225;
/*
 * DFA STATE 1068 (accepts to 57)
 */
state0_1068:
pEnd = pNext;
goto accept0_57;
/*
 * DFA STATE 1069 (accepts to 56)
 */
state0_1069:
pEnd = pNext;
goto accept0_56;
/*
 * DFA STATE 1070 (accepts to 106)
 */
state0_1070:
pEnd = pNext;
goto accept0_106;
/*
 * DFA STATE 1071 (accepts to 143)
 */
state0_1071:
pEnd = pNext;
goto accept0_143;
/*
 * DFA STATE 1072 (accepts to 242)
 */
state0_1072:
pEnd = pNext;
goto accept0_242;
/*
 * DFA STATE 1073 (accepts to 172)
 */
state0_1073:
pEnd = pNext;
goto accept0_172;
/*
 * DFA STATE 1074 (accepts to 229)
 */
state0_1074:
pEnd = pNext;
goto accept0_229;
/*
 * DFA STATE 1075 (accepts to 98)
 */
state0_1075:
pEnd = pNext;
goto accept0_98;
/*
 * DFA STATE 1076 (accepts to 61)
 */
state0_1076:
pEnd = pNext;
goto accept0_61;
/*
 * DFA STATE 1077 (accepts to 63)
 */
state0_1077:
pEnd = pNext;
goto accept0_63;
/*
 * DFA STATE 1078 (accepts to 62)
 */
state0_1078:
pEnd = pNext;
goto accept0_62;
/*
 * DFA STATE 1079
 * ';' -> 1102
 */
state0_1079:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1102;
goto accept0_249;
/*
 * DFA STATE 1080 (accepts to 67)
 */
state0_1080:
pEnd = pNext;
goto accept0_67;
/*
 * DFA STATE 1081 (accepts to 65)
 */
state0_1081:
pEnd = pNext;
goto accept0_65;
/*
 * DFA STATE 1082 (accepts to 196)
 */
state0_1082:
pEnd = pNext;
goto accept0_196;
/*
 * DFA STATE 1083 (accepts to 226)
 */
state0_1083:
pEnd = pNext;
goto accept0_226;
/*
 * DFA STATE 1084 (accepts to 92)
 */
state0_1084:
pEnd = pNext;
goto accept0_92;
/*
 * DFA STATE 1085 (accepts to 243)
 */
state0_1085:
pEnd = pNext;
goto accept0_243;
/*
 * DFA STATE 1086 (accepts to 230)
 */
state0_1086:
pEnd = pNext;
goto accept0_230;
/*
 * DFA STATE 1087 (accepts to 202)
 */
state0_1087:
pEnd = pNext;
goto accept0_202;
/*
 * DFA STATE 1088 (accepts to 150)
 */
state0_1088:
pEnd = pNext;
goto accept0_150;
/*
 * DFA STATE 1089 (accepts to 245)
 */
state0_1089:
pEnd = pNext;
goto accept0_245;
/*
 * DFA STATE 1090 (accepts to 182)
 */
state0_1090:
pEnd = pNext;
goto accept0_182;
/*
 * DFA STATE 1091
 * 'm' -> 1103
 */
state0_1091:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x6D) /* ('l') 'm' */ 
    goto accept0_249;
if(current < 0x6E) /* ('m') 'n' */ 
    goto state0_1103;
goto accept0_249;
/*
 * DFA STATE 1092 (accepts to 209)
 */
state0_1092:
pEnd = pNext;
goto accept0_209;
/*
 * DFA STATE 1093 (accepts to 69)
 */
state0_1093:
pEnd = pNext;
goto accept0_69;
/*
 * DFA STATE 1094 (accepts to 68)
 */
state0_1094:
pEnd = pNext;
goto accept0_68;
/*
 * DFA STATE 1095
 * ';' -> 1104
 */
state0_1095:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1104;
goto accept0_249;
/*
 * DFA STATE 1096 (accepts to 72)
 */
state0_1096:
pEnd = pNext;
goto accept0_72;
/*
 * DFA STATE 1097
 * '[' -> 1105
 */
state0_1097:
if(pNext >= pLimit) goto accept0_3;
current = *pNext++;
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept0_3;
if(current < 0x5C) /* ('[') '\' */ 
    goto state0_1105;
goto accept0_3;
/*
 * DFA STATE 1098 (accepts to 113)
 */
state0_1098:
pEnd = pNext;
goto accept0_113;
/*
 * DFA STATE 1099 (accepts to 123)
 */
state0_1099:
pEnd = pNext;
goto accept0_123;
/*
 * DFA STATE 1100 (accepts to 128)
 */
state0_1100:
pEnd = pNext;
goto accept0_128;
/*
 * DFA STATE 1101 (accepts to 137)
 */
state0_1101:
pEnd = pNext;
goto accept0_137;
/*
 * DFA STATE 1102 (accepts to 147)
 */
state0_1102:
pEnd = pNext;
goto accept0_147;
/*
 * DFA STATE 1103
 * ';' -> 1106
 */
state0_1103:
if(pNext >= pLimit) goto accept0_249;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */ 
    goto accept0_249;
if(current < 0x3C) /* (';') '<' */ 
    goto state0_1106;
goto accept0_249;
/*
 * DFA STATE 1104 (accepts to 153)
 */
state0_1104:
pEnd = pNext;
goto accept0_153;
/*
 * DFA STATE 1105 (accepts to 2)
 */
state0_1105:
pEnd = pNext;
goto accept0_2;
/*
 * DFA STATE 1106 (accepts to 158)
 */
state0_1106:
pEnd = pNext;
goto accept0_158;


accept0_0:
 pNext = pEnd;
 goto inCommentMode; 
accept0_1:
 pNext = pEnd;
 goto inSkipTag; 
accept0_2:
 pNext = pEnd;
 goto inCDataMode; 
accept0_3:
 pNext = pEnd;
 goto inSkipTag; 
accept0_4:
 pNext = pEnd;
 goto inScriptMode; 
accept0_5:
 pNext = pEnd;
 goto inEndTagMode; 
accept0_6:
 pNext = pEnd;
 goto inBeginTagMode; 
accept0_7:
 pNext = pEnd;
 goto textMode; 
accept0_8:
 pNext = pEnd;
 goto textMode; 
accept0_9:
 pNext = pEnd;
 goto textMode; 
accept0_10:
 pNext = pEnd;
 goto textMode; 
accept0_11:
 pNext = pEnd;
 goto textMode; 
accept0_12:
 pNext = pEnd;
 goto textMode; 
accept0_13:
 pNext = pEnd;
 goto textMode; 
accept0_14:
 pNext = pEnd;
 goto textMode; 
accept0_15:
 pNext = pEnd;
 goto textMode; 
accept0_16:
 pNext = pEnd;
 goto textMode; 
accept0_17:
 pNext = pEnd;
 goto textMode; 
accept0_18:
 pNext = pEnd;
 goto textMode; 
accept0_19:
 pNext = pEnd;
 goto textMode; 
accept0_20:
 pNext = pEnd;
 goto textMode; 
accept0_21:
 pNext = pEnd;
 goto textMode; 
accept0_22:
 pNext = pEnd;
 goto textMode; 
accept0_23:
 pNext = pEnd;
 goto textMode; 
accept0_24:
 pNext = pEnd;
 goto textMode; 
accept0_25:
 pNext = pEnd;
 goto textMode; 
accept0_26:
 pNext = pEnd;
 goto textMode; 
accept0_27:
 pNext = pEnd;
 goto textMode; 
accept0_28:
 pNext = pEnd;
 goto textMode; 
accept0_29:
 pNext = pEnd;
 goto textMode; 
accept0_30:
 pNext = pEnd;
 goto textMode; 
accept0_31:
 pNext = pEnd;
 goto textMode; 
accept0_32:
 pNext = pEnd;
 goto textMode; 
accept0_33:
 pNext = pEnd;
 goto textMode; 
accept0_34:
 pNext = pEnd;
 goto textMode; 
accept0_35:
 pNext = pEnd;
 goto textMode; 
accept0_36:
 pNext = pEnd;
 goto textMode; 
accept0_37:
 pNext = pEnd;
 goto textMode; 
accept0_38:
 pNext = pEnd;
 goto textMode; 
accept0_39:
 pNext = pEnd;
 goto textMode; 
accept0_40:
 pNext = pEnd;
 goto textMode; 
accept0_41:
 pNext = pEnd;
 goto textMode; 
accept0_42:
 pNext = pEnd;
 goto textMode; 
accept0_43:
 pNext = pEnd;
 goto textMode; 
accept0_44:
 pNext = pEnd;
 goto textMode; 
accept0_45:
 pNext = pEnd;
 goto textMode; 
accept0_46:
 pNext = pEnd;
 goto textMode; 
accept0_47:
 pNext = pEnd;
 goto textMode; 
accept0_48:
 pNext = pEnd;
 goto textMode; 
accept0_49:
 pNext = pEnd;
 goto textMode; 
accept0_50:
 pNext = pEnd;
 goto textMode; 
accept0_51:
 pNext = pEnd;
 goto textMode; 
accept0_52:
 pNext = pEnd;
 goto textMode; 
accept0_53:
 pNext = pEnd;
 goto textMode; 
accept0_54:
 pNext = pEnd;
 goto textMode; 
accept0_55:
 pNext = pEnd;
 goto textMode; 
accept0_56:
 pNext = pEnd;
 goto textMode; 
accept0_57:
 pNext = pEnd;
 goto textMode; 
accept0_58:
 pNext = pEnd;
 goto textMode; 
accept0_59:
 pNext = pEnd;
 goto textMode; 
accept0_60:
 pNext = pEnd;
 goto textMode; 
accept0_61:
 pNext = pEnd;
 goto textMode; 
accept0_62:
 pNext = pEnd;
 goto textMode; 
accept0_63:
 pNext = pEnd;
 goto textMode; 
accept0_64:
 pNext = pEnd;
 goto textMode; 
accept0_65:
 pNext = pEnd;
 goto textMode; 
accept0_66:
 pNext = pEnd;
 goto textMode; 
accept0_67:
 pNext = pEnd;
 goto textMode; 
accept0_68:
 pNext = pEnd;
 goto textMode; 
accept0_69:
 pNext = pEnd;
 goto textMode; 
accept0_70:
 pNext = pEnd;
 goto textMode; 
accept0_71:
 pNext = pEnd;
 goto textMode; 
accept0_72:
 pNext = pEnd;
 goto textMode; 
accept0_73:
 pNext = pEnd;
 goto textMode; 
accept0_74:
 pNext = pEnd;
 goto textMode; 
accept0_75:
 pNext = pEnd;
 goto textMode; 
accept0_76:
 pNext = pEnd;
 goto textMode; 
accept0_77:
 pNext = pEnd;
 goto textMode; 
accept0_78:
 pNext = pEnd;
 goto textMode; 
accept0_79:
 pNext = pEnd;
 goto textMode; 
accept0_80:
 pNext = pEnd;
 goto textMode; 
accept0_81:
 pNext = pEnd;
 goto textMode; 
accept0_82:
 pNext = pEnd;
 goto textMode; 
accept0_83:
 pNext = pEnd;
 goto textMode; 
accept0_84:
 pNext = pEnd;
 goto textMode; 
accept0_85:
 pNext = pEnd;
 goto textMode; 
accept0_86:
 pNext = pEnd;
 goto textMode; 
accept0_87:
 pNext = pEnd;
 goto textMode; 
accept0_88:
 pNext = pEnd;
 goto textMode; 
accept0_89:
 pNext = pEnd;
 goto textMode; 
accept0_90:
 pNext = pEnd;
 goto textMode; 
accept0_91:
 pNext = pEnd;
 goto textMode; 
accept0_92:
 pNext = pEnd;
 goto textMode; 
accept0_93:
 pNext = pEnd;
 goto textMode; 
accept0_94:
 pNext = pEnd;
 goto textMode; 
accept0_95:
 pNext = pEnd;
 goto textMode; 
accept0_96:
 pNext = pEnd;
 goto textMode; 
accept0_97:
 pNext = pEnd;
 goto textMode; 
accept0_98:
 pNext = pEnd;
 goto textMode; 
accept0_99:
 pNext = pEnd;
 goto textMode; 
accept0_100:
 pNext = pEnd;
 goto textMode; 
accept0_101:
 pNext = pEnd;
 goto textMode; 
accept0_102:
 pNext = pEnd;
 goto textMode; 
accept0_103:
 pNext = pEnd;
 goto textMode; 
accept0_104:
 pNext = pEnd;
 goto textMode; 
accept0_105:
 pNext = pEnd;
 goto textMode; 
accept0_106:
 pNext = pEnd;
 goto textMode; 
accept0_107:
 pNext = pEnd;
 goto textMode; 
accept0_108:
 pNext = pEnd;
 goto textMode; 
accept0_109:
 pNext = pEnd;
 goto textMode; 
accept0_110:
 pNext = pEnd;
 goto textMode; 
accept0_111:
 pNext = pEnd;
 goto textMode; 
accept0_112:
 pNext = pEnd;
 goto textMode; 
accept0_113:
 pNext = pEnd;
 goto textMode; 
accept0_114:
 pNext = pEnd;
 goto textMode; 
accept0_115:
 pNext = pEnd;
 goto textMode; 
accept0_116:
 pNext = pEnd;
 goto textMode; 
accept0_117:
 pNext = pEnd;
 goto textMode; 
accept0_118:
 pNext = pEnd;
 goto textMode; 
accept0_119:
 pNext = pEnd;
 goto textMode; 
accept0_120:
 pNext = pEnd;
 goto textMode; 
accept0_121:
 pNext = pEnd;
 goto textMode; 
accept0_122:
 pNext = pEnd;
 goto textMode; 
accept0_123:
 pNext = pEnd;
 goto textMode; 
accept0_124:
 pNext = pEnd;
 goto textMode; 
accept0_125:
 pNext = pEnd;
 goto textMode; 
accept0_126:
 pNext = pEnd;
 goto textMode; 
accept0_127:
 pNext = pEnd;
 goto textMode; 
accept0_128:
 pNext = pEnd;
 goto textMode; 
accept0_129:
 pNext = pEnd;
 goto textMode; 
accept0_130:
 pNext = pEnd;
 goto textMode; 
accept0_131:
 pNext = pEnd;
 goto textMode; 
accept0_132:
 pNext = pEnd;
 goto textMode; 
accept0_133:
 pNext = pEnd;
 goto textMode; 
accept0_134:
 pNext = pEnd;
 goto textMode; 
accept0_135:
 pNext = pEnd;
 goto textMode; 
accept0_136:
 pNext = pEnd;
 goto textMode; 
accept0_137:
 pNext = pEnd;
 goto textMode; 
accept0_138:
 pNext = pEnd;
 goto textMode; 
accept0_139:
 pNext = pEnd;
 goto textMode; 
accept0_140:
 pNext = pEnd;
 goto textMode; 
accept0_141:
 pNext = pEnd;
 goto textMode; 
accept0_142:
 pNext = pEnd;
 goto textMode; 
accept0_143:
 pNext = pEnd;
 goto textMode; 
accept0_144:
 pNext = pEnd;
 goto textMode; 
accept0_145:
 pNext = pEnd;
 goto textMode; 
accept0_146:
 pNext = pEnd;
 goto textMode; 
accept0_147:
 pNext = pEnd;
 goto textMode; 
accept0_148:
 pNext = pEnd;
 goto textMode; 
accept0_149:
 pNext = pEnd;
 goto textMode; 
accept0_150:
 pNext = pEnd;
 goto textMode; 
accept0_151:
 pNext = pEnd;
 goto textMode; 
accept0_152:
 pNext = pEnd;
 goto textMode; 
accept0_153:
 pNext = pEnd;
 goto textMode; 
accept0_154:
 pNext = pEnd;
 goto textMode; 
accept0_155:
 pNext = pEnd;
 goto textMode; 
accept0_156:
 pNext = pEnd;
 goto textMode; 
accept0_157:
 pNext = pEnd;
 goto textMode; 
accept0_158:
 pNext = pEnd;
 goto textMode; 
accept0_159:
 pNext = pEnd;
 goto textMode; 
accept0_160:
 pNext = pEnd;
 goto textMode; 
accept0_161:
 pNext = pEnd;
 goto textMode; 
accept0_162:
 pNext = pEnd;
 goto textMode; 
accept0_163:
 pNext = pEnd;
 goto textMode; 
accept0_164:
 pNext = pEnd;
 goto textMode; 
accept0_165:
 pNext = pEnd;
 goto textMode; 
accept0_166:
 pNext = pEnd;
 goto textMode; 
accept0_167:
 pNext = pEnd;
 goto textMode; 
accept0_168:
 pNext = pEnd;
 goto textMode; 
accept0_169:
 pNext = pEnd;
 goto textMode; 
accept0_170:
 pNext = pEnd;
 goto textMode; 
accept0_171:
 pNext = pEnd;
 goto textMode; 
accept0_172:
 pNext = pEnd;
 goto textMode; 
accept0_173:
 pNext = pEnd;
 goto textMode; 
accept0_174:
 pNext = pEnd;
 goto textMode; 
accept0_175:
 pNext = pEnd;
 goto textMode; 
accept0_176:
 pNext = pEnd;
 goto textMode; 
accept0_177:
 pNext = pEnd;
 goto textMode; 
accept0_178:
 pNext = pEnd;
 goto textMode; 
accept0_179:
 pNext = pEnd;
 goto textMode; 
accept0_180:
 pNext = pEnd;
 goto textMode; 
accept0_181:
 pNext = pEnd;
 goto textMode; 
accept0_182:
 pNext = pEnd;
 goto textMode; 
accept0_183:
 pNext = pEnd;
 goto textMode; 
accept0_184:
 pNext = pEnd;
 goto textMode; 
accept0_185:
 pNext = pEnd;
 goto textMode; 
accept0_186:
 pNext = pEnd;
 goto textMode; 
accept0_187:
 pNext = pEnd;
 goto textMode; 
accept0_188:
 pNext = pEnd;
 goto textMode; 
accept0_189:
 pNext = pEnd;
 goto textMode; 
accept0_190:
 pNext = pEnd;
 goto textMode; 
accept0_191:
 pNext = pEnd;
 goto textMode; 
accept0_192:
 pNext = pEnd;
 goto textMode; 
accept0_193:
 pNext = pEnd;
 goto textMode; 
accept0_194:
 pNext = pEnd;
 goto textMode; 
accept0_195:
 pNext = pEnd;
 goto textMode; 
accept0_196:
 pNext = pEnd;
 goto textMode; 
accept0_197:
 pNext = pEnd;
 goto textMode; 
accept0_198:
 pNext = pEnd;
 goto textMode; 
accept0_199:
 pNext = pEnd;
 goto textMode; 
accept0_200:
 pNext = pEnd;
 goto textMode; 
accept0_201:
 pNext = pEnd;
 goto textMode; 
accept0_202:
 pNext = pEnd;
 goto textMode; 
accept0_203:
 pNext = pEnd;
 goto textMode; 
accept0_204:
 pNext = pEnd;
 goto textMode; 
accept0_205:
 pNext = pEnd;
 goto textMode; 
accept0_206:
 pNext = pEnd;
 goto textMode; 
accept0_207:
 pNext = pEnd;
 goto textMode; 
accept0_208:
 pNext = pEnd;
 goto textMode; 
accept0_209:
 pNext = pEnd;
 goto textMode; 
accept0_210:
 pNext = pEnd;
 goto textMode; 
accept0_211:
 pNext = pEnd;
 goto textMode; 
accept0_212:
 pNext = pEnd;
 goto textMode; 
accept0_213:
 pNext = pEnd;
 goto textMode; 
accept0_214:
 pNext = pEnd;
 goto textMode; 
accept0_215:
 pNext = pEnd;
 goto textMode; 
accept0_216:
 pNext = pEnd;
 goto textMode; 
accept0_217:
 pNext = pEnd;
 goto textMode; 
accept0_218:
 pNext = pEnd;
 goto textMode; 
accept0_219:
 pNext = pEnd;
 goto textMode; 
accept0_220:
 pNext = pEnd;
 goto textMode; 
accept0_221:
 pNext = pEnd;
 goto textMode; 
accept0_222:
 pNext = pEnd;
 goto textMode; 
accept0_223:
 pNext = pEnd;
 goto textMode; 
accept0_224:
 pNext = pEnd;
 goto textMode; 
accept0_225:
 pNext = pEnd;
 goto textMode; 
accept0_226:
 pNext = pEnd;
 goto textMode; 
accept0_227:
 pNext = pEnd;
 goto textMode; 
accept0_228:
 pNext = pEnd;
 goto textMode; 
accept0_229:
 pNext = pEnd;
 goto textMode; 
accept0_230:
 pNext = pEnd;
 goto textMode; 
accept0_231:
 pNext = pEnd;
 goto textMode; 
accept0_232:
 pNext = pEnd;
 goto textMode; 
accept0_233:
 pNext = pEnd;
 goto textMode; 
accept0_234:
 pNext = pEnd;
 goto textMode; 
accept0_235:
 pNext = pEnd;
 goto textMode; 
accept0_236:
 pNext = pEnd;
 goto textMode; 
accept0_237:
 pNext = pEnd;
 goto textMode; 
accept0_238:
 pNext = pEnd;
 goto textMode; 
accept0_239:
 pNext = pEnd;
 goto textMode; 
accept0_240:
 pNext = pEnd;
 goto textMode; 
accept0_241:
 pNext = pEnd;
 goto textMode; 
accept0_242:
 pNext = pEnd;
 goto textMode; 
accept0_243:
 pNext = pEnd;
 goto textMode; 
accept0_244:
 pNext = pEnd;
 goto textMode; 
accept0_245:
 pNext = pEnd;
 goto textMode; 
accept0_246:
 pNext = pEnd;
 goto textMode; 
accept0_247:
 pNext = pEnd;
 goto textMode; 
accept0_248:
 pNext = pEnd;
 goto textMode; 
accept0_249:
 pNext = pEnd;
 goto textMode; 
nonaccept0:
 return 0; }
        inBeginTagMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Concat
 *  |     +-Ranges(['a'-'z'],['A'-'Z'])
 *  |     +-Repeat(ZeroOrMore)
 *  |        +-Ranges(['a'-'z'],['A'-'Z'],['0'-'9'],':','-')
 *  +-Accept(1)
 *  |  +-Sequence(CaseInsensitive,'>')
 *  +-Accept(2)
 *     +-Sequence(CaseInsensitive,'/>')
 */
/*
 * DFA STATE 0
 * '/' -> 1
 * '>' -> 2
 * ['A'-'Z'] -> 3
 * ['a'-'z'] -> 3
 */
if(pNext >= pLimit) goto nonaccept1;
var current = *pNext++;
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2F) /* ('.') '/' */ 
            goto nonaccept1;
        goto state1_1;
    }
    if(current < 0x3E) /* ('=') '>' */ 
        goto nonaccept1;
    goto state1_2;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto nonaccept1;
    goto state1_3;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto nonaccept1;
if(current < 0x7B) /* ('z') '{' */ 
    goto state1_3;
goto nonaccept1;
/*
 * DFA STATE 1
 * '>' -> 4
 */
state1_1:
if(pNext >= pLimit) goto nonaccept1;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto nonaccept1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state1_4;
goto nonaccept1;
/*
 * DFA STATE 2 (accepts to 1)
 */
state1_2:
pEnd = pNext;
goto accept1_1;
/*
 * DFA STATE 3 (accepts to 0)
 * '-' -> 5
 * ['0'-'9'] -> 5
 * ':' -> 5
 * ['A'-'Z'] -> 5
 * ['a'-'z'] -> 5
 */
state1_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_0;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept1_0;
        goto state1_5;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_0;
    goto state1_5;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept1_0;
    goto state1_5;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept1_0;
if(current < 0x7B) /* ('z') '{' */ 
    goto state1_5;
goto accept1_0;
/*
 * DFA STATE 4 (accepts to 2)
 */
state1_4:
pEnd = pNext;
goto accept1_2;
/*
 * DFA STATE 5 (accepts to 0)
 * '-' -> 5
 * ['0'-'9'] -> 5
 * ':' -> 5
 * ['A'-'Z'] -> 5
 * ['a'-'z'] -> 5
 */
state1_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_0;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept1_0;
        goto state1_5;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_0;
    goto state1_5;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept1_0;
    goto state1_5;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept1_0;
if(current < 0x7B) /* ('z') '{' */ 
    goto state1_5;
goto accept1_0;


accept1_0:
 pNext = pEnd;
 goto inBeginTagAttributeMode; 
accept1_1:
 pNext = pEnd;
 goto textMode; 
accept1_2:
 pNext = pEnd;
 goto textMode; 
nonaccept1:
 return 0; }
        inBeginTagAttributeMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(' ',0x0A,0x0D,0x09)
 *  +-Accept(1)
 *  |  +-Concat
 *  |     +-Ranges(['a'-'z'],['A'-'Z'])
 *  |     +-Repeat(ZeroOrMore)
 *  |        +-Ranges(['a'-'z'],['A'-'Z'],['0'-'9'],':','-')
 *  +-Accept(2)
 *  |  +-Sequence(CaseInsensitive,'=')
 *  +-Accept(3)
 *  |  +-Sequence(CaseInsensitive,'>')
 *  +-Accept(4)
 *     +-Sequence(CaseInsensitive,'/>')
 */
/*
 * DFA STATE 0
 * 0x09 -> 1
 * 0x0A -> 1
 * 0x0D -> 1
 * ' ' -> 1
 * '/' -> 2
 * '=' -> 3
 * '>' -> 4
 * ['A'-'Z'] -> 5
 * ['a'-'z'] -> 5
 */
if(pNext >= pLimit) goto nonaccept2;
var current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0xE) {
        if(current < 0xB) {
            if(current < 0x9)
                goto nonaccept2;
            goto state2_1;
        }
        if(current < 0xD)
            goto nonaccept2;
        goto state2_1;
    }
    if(current < 0x21) /* (' ') '!' */  {
        if(current < 0x20)
            goto nonaccept2;
        goto state2_1;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto nonaccept2;
    goto state2_2;
}
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x3E) /* ('=') '>' */  {
        if(current < 0x3D) /* ('<') '=' */ 
            goto nonaccept2;
        goto state2_3;
    }
    if(current < 0x3F) /* ('>') '?' */ 
        goto state2_4;
    goto nonaccept2;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state2_5;
    goto nonaccept2;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state2_5;
goto nonaccept2;
/*
 * DFA STATE 1 (accepts to 0)
 */
state2_1:
pEnd = pNext;
goto accept2_0;
/*
 * DFA STATE 2
 * '>' -> 6
 */
state2_2:
if(pNext >= pLimit) goto nonaccept2;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto nonaccept2;
if(current < 0x3F) /* ('>') '?' */ 
    goto state2_6;
goto nonaccept2;
/*
 * DFA STATE 3 (accepts to 2)
 */
state2_3:
pEnd = pNext;
goto accept2_2;
/*
 * DFA STATE 4 (accepts to 3)
 */
state2_4:
pEnd = pNext;
goto accept2_3;
/*
 * DFA STATE 5 (accepts to 1)
 * '-' -> 7
 * ['0'-'9'] -> 7
 * ':' -> 7
 * ['A'-'Z'] -> 7
 * ['a'-'z'] -> 7
 */
state2_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept2_1;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept2_1;
        goto state2_7;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept2_1;
    goto state2_7;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept2_1;
    goto state2_7;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept2_1;
if(current < 0x7B) /* ('z') '{' */ 
    goto state2_7;
goto accept2_1;
/*
 * DFA STATE 6 (accepts to 4)
 */
state2_6:
pEnd = pNext;
goto accept2_4;
/*
 * DFA STATE 7 (accepts to 1)
 * '-' -> 7
 * ['0'-'9'] -> 7
 * ':' -> 7
 * ['A'-'Z'] -> 7
 * ['a'-'z'] -> 7
 */
state2_7:
pEnd = pNext;
if(pNext >= pLimit) goto accept2_1;
current = *pNext++;
if(current < 0x3B) /* (':') ';' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto accept2_1;
        goto state2_7;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept2_1;
    goto state2_7;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x41) /* ('@') 'A' */ 
        goto accept2_1;
    goto state2_7;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto accept2_1;
if(current < 0x7B) /* ('z') '{' */ 
    goto state2_7;
goto accept2_1;


accept2_0:
 pNext = pEnd;
 goto inBeginTagAttributeMode; 
accept2_1:
 pNext = pEnd;
 goto inBeginTagAttributeMode; 
accept2_2:
 pNext = pEnd;
 goto inBeginTagAttributeValueMode; 
accept2_3:
 pNext = pEnd;
 goto textMode; 
accept2_4:
 pNext = pEnd;
 goto textMode; 
nonaccept2:
 return 0; }
        inBeginTagAttributeValueMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(' ',0x0A,0x0D,0x09)
 *  +-Accept(1)
 *  |  +-Sequence(CaseInsensitive,'>')
 *  +-Accept(2)
 *  |  +-Sequence(CaseInsensitive,'/>')
 *  +-Accept(3)
 *  |  +-Concat
 *  |     +-Ranges(''')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges([0x00-'&'],['('-0xFFFF])
 *  |     +-Ranges(''')
 *  +-Accept(4)
 *  |  +-Concat
 *  |     +-Ranges('"')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges([0x00-'!'],['#'-0xFFFF])
 *  |     +-Ranges('"')
 *  +-Accept(5)
 *     +-Repeat(OneOrMore)
 *        +-Ranges([0x00-0x08],[0x0B-''],'!',['#'-'&'],['('-'.'],['0'-'<'],['?'-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-0x08] -> 1
 * 0x09 -> 2
 * 0x0A -> 2
 * [0x0B-0x0C] -> 1
 * 0x0D -> 3
 * [0x0E-''] -> 1
 * ' ' -> 2
 * '!' -> 1
 * '"' -> 4
 * ['#'-'&'] -> 1
 * ''' -> 5
 * ['('-'.'] -> 1
 * '/' -> 6
 * ['0'-'<'] -> 1
 * '>' -> 7
 * ['?'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept3;
var current = *pNext++;
if(current < 0x23) /* ('"') '#' */  {
    if(current < 0xE) {
        if(current < 0xB) {
            if(current < 0x9)
                goto state3_1;
            goto state3_2;
        }
        if(current < 0xD)
            goto state3_1;
        goto state3_3;
    }
    if(current < 0x21) /* (' ') '!' */  {
        if(current < 0x20)
            goto state3_1;
        goto state3_2;
    }
    if(current < 0x22) /* ('!') '"' */ 
        goto state3_1;
    goto state3_4;
}
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x28) /* (''') '(' */  {
        if(current < 0x27) /* ('&') ''' */ 
            goto state3_1;
        goto state3_5;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state3_1;
    goto state3_6;
}
if(current < 0x3E) /* ('=') '>' */  {
    if(current < 0x3D) /* ('<') '=' */ 
        goto state3_1;
    goto nonaccept3;
}
if(current < 0x3F) /* ('>') '?' */ 
    goto state3_7;
goto state3_1;
/*
 * DFA STATE 1 (accepts to 5)
 * [0x00-0x08] -> 1
 * [0x0B-''] -> 1
 * '!' -> 1
 * ['#'-'&'] -> 1
 * ['('-'.'] -> 1
 * ['0'-'<'] -> 1
 * ['?'-0xFFFF] -> 1
 */
state3_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept3_5;
current = *pNext++;
if(current < 0x23) /* ('"') '#' */  {
    if(current < 0x20) {
        if(current < 0x9)
            goto state3_1;
        if(current < 0xB)
            goto accept3_5;
        goto state3_1;
    }
    if(current < 0x21) /* (' ') '!' */ 
        goto accept3_5;
    if(current < 0x22) /* ('!') '"' */ 
        goto state3_1;
    goto accept3_5;
}
if(current < 0x2F) /* ('.') '/' */  {
    if(current < 0x27) /* ('&') ''' */ 
        goto state3_1;
    if(current < 0x28) /* (''') '(' */ 
        goto accept3_5;
    goto state3_1;
}
if(current < 0x3D) /* ('<') '=' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept3_5;
    goto state3_1;
}
if(current < 0x3F) /* ('>') '?' */ 
    goto accept3_5;
goto state3_1;
/*
 * DFA STATE 2 (accepts to 0)
 */
state3_2:
pEnd = pNext;
goto accept3_0;
/*
 * DFA STATE 3 (accepts to 0)
 */
state3_3:
pEnd = pNext;
goto accept3_0;
/*
 * DFA STATE 4
 * [0x00-'!'] -> 8
 * '"' -> 9
 * ['#'-0xFFFF] -> 8
 */
state3_4:
if(pNext >= pLimit) goto nonaccept3;
current = *pNext++;
if(current < 0x22) /* ('!') '"' */ 
    goto state3_8;
if(current < 0x23) /* ('"') '#' */ 
    goto state3_9;
goto state3_8;
/*
 * DFA STATE 5
 * [0x00-'&'] -> 10
 * ''' -> 11
 * ['('-0xFFFF] -> 10
 */
state3_5:
if(pNext >= pLimit) goto nonaccept3;
current = *pNext++;
if(current < 0x27) /* ('&') ''' */ 
    goto state3_10;
if(current < 0x28) /* (''') '(' */ 
    goto state3_11;
goto state3_10;
/*
 * DFA STATE 6
 * '>' -> 12
 */
state3_6:
if(pNext >= pLimit) goto nonaccept3;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto nonaccept3;
if(current < 0x3F) /* ('>') '?' */ 
    goto state3_12;
goto nonaccept3;
/*
 * DFA STATE 7 (accepts to 1)
 */
state3_7:
pEnd = pNext;
goto accept3_1;
/*
 * DFA STATE 8
 * [0x00-'!'] -> 8
 * '"' -> 9
 * ['#'-0xFFFF] -> 8
 */
state3_8:
if(pNext >= pLimit) goto nonaccept3;
current = *pNext++;
if(current < 0x22) /* ('!') '"' */ 
    goto state3_8;
if(current < 0x23) /* ('"') '#' */ 
    goto state3_9;
goto state3_8;
/*
 * DFA STATE 9 (accepts to 4)
 */
state3_9:
pEnd = pNext;
goto accept3_4;
/*
 * DFA STATE 10
 * [0x00-'&'] -> 10
 * ''' -> 11
 * ['('-0xFFFF] -> 10
 */
state3_10:
if(pNext >= pLimit) goto nonaccept3;
current = *pNext++;
if(current < 0x27) /* ('&') ''' */ 
    goto state3_10;
if(current < 0x28) /* (''') '(' */ 
    goto state3_11;
goto state3_10;
/*
 * DFA STATE 11 (accepts to 3)
 */
state3_11:
pEnd = pNext;
goto accept3_3;
/*
 * DFA STATE 12 (accepts to 2)
 */
state3_12:
pEnd = pNext;
goto accept3_2;


accept3_0:
 pNext = pEnd;
 goto inBeginTagAttributeValueMode; 
accept3_1:
 pNext = pEnd;
 goto textMode; 
accept3_2:
 pNext = pEnd;
 goto textMode; 
accept3_3:
 pNext = pEnd;
 goto inBeginTagMode; 
accept3_4:
 pNext = pEnd;
 goto inBeginTagMode; 
accept3_5:
 pNext = pEnd;
 goto inBeginTagMode; 
nonaccept3:
 return 0; }
        inEndTagMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges('>')
 *  +-Accept(1)
 *     +-Repeat(OneOrMore)
 *        +-Ranges([0x00-'='],['?'-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'='] -> 1
 * '>' -> 2
 * ['?'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept4;
var current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto state4_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state4_2;
goto state4_1;
/*
 * DFA STATE 1 (accepts to 1)
 * [0x00-'='] -> 1
 * ['?'-0xFFFF] -> 1
 */
state4_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept4_1;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto state4_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto accept4_1;
goto state4_1;
/*
 * DFA STATE 2 (accepts to 0)
 */
state4_2:
pEnd = pNext;
goto accept4_0;


accept4_0:
 pNext = pEnd;
 goto textMode; 
accept4_1:
 pNext = pEnd;
 goto inEndTagMode; 
nonaccept4:
 return 0; }
        inScriptMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Sequence(CaseInsensitive,'>')
 *  +-Accept(1)
 *  |  +-Sequence(CaseInsensitive,'/>')
 *  +-Accept(2)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'.'] -> 1
 * '/' -> 2
 * ['0'-'='] -> 1
 * '>' -> 3
 * ['?'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept5;
var current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_1;
    goto state5_2;
}
if(current < 0x3E) /* ('=') '>' */ 
    goto state5_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state5_3;
goto state5_1;
/*
 * DFA STATE 1 (accepts to 2)
 */
state5_1:
pEnd = pNext;
goto accept5_2;
/*
 * DFA STATE 2 (accepts to 2)
 * '>' -> 4
 */
state5_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_2;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto accept5_2;
if(current < 0x3F) /* ('>') '?' */ 
    goto state5_4;
goto accept5_2;
/*
 * DFA STATE 3 (accepts to 0)
 */
state5_3:
pEnd = pNext;
goto accept5_0;
/*
 * DFA STATE 4 (accepts to 1)
 */
state5_4:
pEnd = pNext;
goto accept5_1;


accept5_0:
 pNext = pEnd;
 goto inScriptBodyMode; 
accept5_1:
 pNext = pEnd;
 goto textMode; 
accept5_2:
 pNext = pEnd;
 goto inScriptMode; 
nonaccept5:
 return 0; }
        inScriptBodyMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Concat
 *  |     +-Sequence(CaseInsensitive,'</')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges(' ',0x0A,0x0D,0x09)
 *  |     +-Sequence(CaseInsensitive,'script')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges(' ',0x0A,0x0D,0x09)
 *  |     +-Sequence(CaseInsensitive,'>')
 *  +-Accept(1)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-';'] -> 1
 * '<' -> 2
 * ['='-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept6;
var current = *pNext++;
if(current < 0x3C) /* (';') '<' */ 
    goto state6_1;
if(current < 0x3D) /* ('<') '=' */ 
    goto state6_2;
goto state6_1;
/*
 * DFA STATE 1 (accepts to 1)
 */
state6_1:
pEnd = pNext;
goto accept6_1;
/*
 * DFA STATE 2 (accepts to 1)
 * '/' -> 3
 */
state6_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x2F) /* ('.') '/' */ 
    goto accept6_1;
if(current < 0x30) /* ('/') '0' */ 
    goto state6_3;
goto accept6_1;
/*
 * DFA STATE 3
 * 0x09 -> 4
 * 0x0A -> 4
 * 0x0D -> 4
 * ' ' -> 4
 * 'S' -> 5
 * 's' -> 5
 */
state6_3:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x20) {
    if(current < 0xB) {
        if(current < 0x9)
            goto accept6_1;
        goto state6_4;
    }
    if(current < 0xD)
        goto accept6_1;
    if(current < 0xE)
        goto state6_4;
    goto accept6_1;
}
if(current < 0x54) /* ('S') 'T' */  {
    if(current < 0x21) /* (' ') '!' */ 
        goto state6_4;
    if(current < 0x53) /* ('R') 'S' */ 
        goto accept6_1;
    goto state6_5;
}
if(current < 0x73) /* ('r') 's' */ 
    goto accept6_1;
if(current < 0x74) /* ('s') 't' */ 
    goto state6_5;
goto accept6_1;
/*
 * DFA STATE 4
 * 0x09 -> 4
 * 0x0A -> 4
 * 0x0D -> 4
 * ' ' -> 4
 * 'S' -> 5
 * 's' -> 5
 */
state6_4:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x20) {
    if(current < 0xB) {
        if(current < 0x9)
            goto accept6_1;
        goto state6_4;
    }
    if(current < 0xD)
        goto accept6_1;
    if(current < 0xE)
        goto state6_4;
    goto accept6_1;
}
if(current < 0x54) /* ('S') 'T' */  {
    if(current < 0x21) /* (' ') '!' */ 
        goto state6_4;
    if(current < 0x53) /* ('R') 'S' */ 
        goto accept6_1;
    goto state6_5;
}
if(current < 0x73) /* ('r') 's' */ 
    goto accept6_1;
if(current < 0x74) /* ('s') 't' */ 
    goto state6_5;
goto accept6_1;
/*
 * DFA STATE 5
 * 'C' -> 6
 * 'c' -> 6
 */
state6_5:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x44) /* ('C') 'D' */  {
    if(current < 0x43) /* ('B') 'C' */ 
        goto accept6_1;
    goto state6_6;
}
if(current < 0x63) /* ('b') 'c' */ 
    goto accept6_1;
if(current < 0x64) /* ('c') 'd' */ 
    goto state6_6;
goto accept6_1;
/*
 * DFA STATE 6
 * 'R' -> 7
 * 'r' -> 7
 */
state6_6:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x53) /* ('R') 'S' */  {
    if(current < 0x52) /* ('Q') 'R' */ 
        goto accept6_1;
    goto state6_7;
}
if(current < 0x72) /* ('q') 'r' */ 
    goto accept6_1;
if(current < 0x73) /* ('r') 's' */ 
    goto state6_7;
goto accept6_1;
/*
 * DFA STATE 7
 * 'I' -> 8
 * 'i' -> 8
 */
state6_7:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x4A) /* ('I') 'J' */  {
    if(current < 0x49) /* ('H') 'I' */ 
        goto accept6_1;
    goto state6_8;
}
if(current < 0x69) /* ('h') 'i' */ 
    goto accept6_1;
if(current < 0x6A) /* ('i') 'j' */ 
    goto state6_8;
goto accept6_1;
/*
 * DFA STATE 8
 * 'P' -> 9
 * 'p' -> 9
 */
state6_8:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x51) /* ('P') 'Q' */  {
    if(current < 0x50) /* ('O') 'P' */ 
        goto accept6_1;
    goto state6_9;
}
if(current < 0x70) /* ('o') 'p' */ 
    goto accept6_1;
if(current < 0x71) /* ('p') 'q' */ 
    goto state6_9;
goto accept6_1;
/*
 * DFA STATE 9
 * 'T' -> 10
 * 't' -> 10
 */
state6_9:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0x55) /* ('T') 'U' */  {
    if(current < 0x54) /* ('S') 'T' */ 
        goto accept6_1;
    goto state6_10;
}
if(current < 0x74) /* ('s') 't' */ 
    goto accept6_1;
if(current < 0x75) /* ('t') 'u' */ 
    goto state6_10;
goto accept6_1;
/*
 * DFA STATE 10
 * 0x09 -> 11
 * 0x0A -> 11
 * 0x0D -> 11
 * ' ' -> 11
 * '>' -> 12
 */
state6_10:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0xE) {
    if(current < 0xB) {
        if(current < 0x9)
            goto accept6_1;
        goto state6_11;
    }
    if(current < 0xD)
        goto accept6_1;
    goto state6_11;
}
if(current < 0x21) /* (' ') '!' */  {
    if(current < 0x20)
        goto accept6_1;
    goto state6_11;
}
if(current < 0x3E) /* ('=') '>' */ 
    goto accept6_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state6_12;
goto accept6_1;
/*
 * DFA STATE 11
 * 0x09 -> 11
 * 0x0A -> 11
 * 0x0D -> 11
 * ' ' -> 11
 * '>' -> 12
 */
state6_11:
if(pNext >= pLimit) goto accept6_1;
current = *pNext++;
if(current < 0xE) {
    if(current < 0xB) {
        if(current < 0x9)
            goto accept6_1;
        goto state6_11;
    }
    if(current < 0xD)
        goto accept6_1;
    goto state6_11;
}
if(current < 0x21) /* (' ') '!' */  {
    if(current < 0x20)
        goto accept6_1;
    goto state6_11;
}
if(current < 0x3E) /* ('=') '>' */ 
    goto accept6_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state6_12;
goto accept6_1;
/*
 * DFA STATE 12 (accepts to 0)
 */
state6_12:
pEnd = pNext;
goto accept6_0;


accept6_0:
 pNext = pEnd;
 goto textMode; 
accept6_1:
 pNext = pEnd;
 goto inScriptBodyMode; 
nonaccept6:
 return 0; }
        inCDataMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(']',']','>')
 *  +-Accept(1)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges([0x00-'\'],['^'-0xFFFF])
 *  +-Accept(2)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'='] -> 1
 * '>' -> 2
 * ['?'-'\'] -> 1
 * ']' -> 3
 * ['^'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept7;
var current = *pNext++;
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x3E) /* ('=') '>' */ 
        goto state7_1;
    goto state7_2;
}
if(current < 0x5D) /* ('\') ']' */ 
    goto state7_1;
if(current < 0x5E) /* (']') '^' */ 
    goto state7_3;
goto state7_1;
/*
 * DFA STATE 1 (accepts to 1)
 * [0x00-'\'] -> 4
 * ['^'-0xFFFF] -> 4
 */
state7_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept7_1;
current = *pNext++;
if(current < 0x5D) /* ('\') ']' */ 
    goto state7_4;
if(current < 0x5E) /* (']') '^' */ 
    goto accept7_1;
goto state7_4;
/*
 * DFA STATE 2 (accepts to 0)
 */
state7_2:
pEnd = pNext;
goto accept7_0;
/*
 * DFA STATE 3 (accepts to 0)
 */
state7_3:
pEnd = pNext;
goto accept7_0;
/*
 * DFA STATE 4 (accepts to 1)
 * [0x00-'\'] -> 4
 * ['^'-0xFFFF] -> 4
 */
state7_4:
pEnd = pNext;
if(pNext >= pLimit) goto accept7_1;
current = *pNext++;
if(current < 0x5D) /* ('\') ']' */ 
    goto state7_4;
if(current < 0x5E) /* (']') '^' */ 
    goto accept7_1;
goto state7_4;


accept7_0:
 pNext = pEnd;
 goto textMode; 
accept7_1:
 pNext = pEnd;
 goto inCDataMode; 
accept7_2:
 pNext = pEnd;
 goto inCDataMode; 
nonaccept7:
 return 0; }
        inSkipTag:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges('>')
 *  +-Accept(1)
 *     +-Repeat(OneOrMore)
 *        +-Ranges([0x00-'='],['?'-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'='] -> 1
 * '>' -> 2
 * ['?'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept8;
var current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto state8_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto state8_2;
goto state8_1;
/*
 * DFA STATE 1 (accepts to 1)
 * [0x00-'='] -> 1
 * ['?'-0xFFFF] -> 1
 */
state8_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept8_1;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto state8_1;
if(current < 0x3F) /* ('>') '?' */ 
    goto accept8_1;
goto state8_1;
/*
 * DFA STATE 2 (accepts to 0)
 */
state8_2:
pEnd = pNext;
goto accept8_0;


accept8_0:
 pNext = pEnd;
 goto textMode; 
accept8_1:
 pNext = pEnd;
 goto inCommentMode; 
nonaccept8:
 return 0; }
        inCommentMode:
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Sequence(CaseInsensitive,'-->')
 *  +-Accept(1)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges([0x00-','],['.'-0xFFFF])
 *  +-Accept(2)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-','] -> 1
 * '-' -> 2
 * ['.'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept9;
var current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto state9_1;
if(current < 0x2E) /* ('-') '.' */ 
    goto state9_2;
goto state9_1;
/*
 * DFA STATE 1 (accepts to 1)
 * [0x00-','] -> 3
 * ['.'-0xFFFF] -> 3
 */
state9_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept9_1;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto state9_3;
if(current < 0x2E) /* ('-') '.' */ 
    goto accept9_1;
goto state9_3;
/*
 * DFA STATE 2 (accepts to 2)
 * '-' -> 4
 */
state9_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept9_2;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept9_2;
if(current < 0x2E) /* ('-') '.' */ 
    goto state9_4;
goto accept9_2;
/*
 * DFA STATE 3 (accepts to 1)
 * [0x00-','] -> 3
 * ['.'-0xFFFF] -> 3
 */
state9_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept9_1;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto state9_3;
if(current < 0x2E) /* ('-') '.' */ 
    goto accept9_1;
goto state9_3;
/*
 * DFA STATE 4
 * '>' -> 5
 */
state9_4:
if(pNext >= pLimit) goto accept9_2;
current = *pNext++;
if(current < 0x3E) /* ('=') '>' */ 
    goto accept9_2;
if(current < 0x3F) /* ('>') '?' */ 
    goto state9_5;
goto accept9_2;
/*
 * DFA STATE 5 (accepts to 0)
 */
state9_5:
pEnd = pNext;
goto accept9_0;


accept9_0:
 pNext = pEnd;
 goto textMode; 
accept9_1:
 pNext = pEnd;
 goto inCommentMode; 
accept9_2:
 pNext = pEnd;
 goto inCommentMode; 
nonaccept9:
 return 0; }
        }
    }
}
