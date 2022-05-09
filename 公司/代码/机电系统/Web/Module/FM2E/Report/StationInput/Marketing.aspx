<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Marketing.aspx.cs" Inherits="Module_FM2E_Report_Input_Marketing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript" charset="gbk">
		function jump(raq,station){
		    var url = "http://";
		    var ip1="<%=quieeIP1%>";
		    var ip2="<%=quieeIP2%>";
		    var tempurl =document.location.href;
		    if (tempurl.indexOf(ip2)>0) {
		        url += "<%=quieeIP1%>";
		    }
		    else{
		    	url += "<%=quieeIP%>";	    
		    }
		    url += "/quiee/reportJsp/showReport.jsp"
            if (raq!="") url += "?raq=/" + raq;
            if ("<%=station%>"!="") url += "&station=" + "<%=station%>";
            window.open(url);
		}
        </script>
	</head>

	<body>
		<div id="main">
		<!--
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z" style="font-family: 微软雅黑; color: #000000;">
						直管路段车流量、路费收入分析汇总表
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_meiguan_TrafficVolumeIncomeStatistical_i.raq','')"
								value="梅观" style="font-family: 微软雅黑; height: 30px;  width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_jihedong_TrafficVolumeIncomeStatistical_i.raq','')"
								value="机荷东" style="font-family: 微软雅黑; height: 30px;  width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_yanba_TrafficVolumeIncomeStatistical_i.raq','')"
								value="盐坝" style="font-family: 微软雅黑; height: 30px;  width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_jihexi_TrafficVolumeIncomeStatistical_i.raq','')"
								value="机荷西" style="font-family: 微软雅黑; height: 30px;   width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_yanpai_TrafficVolumeIncomeStatistical_i.raq','')"
								value="盐排" style="font-family: 微软雅黑; height: 30px;   width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Zhiguan_nanguang_TrafficVolumeIncomeStatistical_i.raq','')"
								value="南光" style="font-family: 微软雅黑; height: 30px;   width: 68px;cursor: hand;"/>
						</li>
					</ul>
				</div>
			</div>
			-->
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						深圳区域路网特殊车辆分类统计
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						
						<li>
							<input type="button" id="meilin" runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_meiguan_Meilin_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="梅林站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="banhua" runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_meiguan_Banhua_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="坂华站" style="font-family: 微软雅黑;height: 30px; width: 68px;cursor: hand;" />
						</li>
						<li>
							<input type="button" id= "guanlan"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_meiguan_Guanlan_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="观澜站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "dameisha"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_yanba_Dameisha_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="大梅沙站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "xiaomeisha"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_yanba_Xiaomeisha_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="小梅沙站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;" />
						</li>
						<li>
							<input type="button"  id= "kuichong"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_yanba_Kuichong_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="葵涌站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "yantian"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_yanpai_Yantian_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="盐田站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "heao"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihedong_Heao_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="荷坳站" style="font-family: 微软雅黑;height: 30px; width: 68px;cursor: hand;" />
						</li>
						<li>
							<input type="button"  id= "bainikeng"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihedong_Bainikeng_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="白泥坑站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "fumin"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihedong_Fumin_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="福民站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "huanghe"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihexi_Huanghe_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="黄鹤站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;" />
						</li>
						<li>
							<input type="button"  id= "shuilang"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihexi_Shuilang_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="水朗站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li><li>
							<input type="button"  id= "shiyan"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_jihexi_Shiyan_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="石岩站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id= "xili"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_nanguang_Xili_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="西丽站" style="font-family: 微软雅黑;height: 30px; width: 68px;cursor: hand;" />
						</li>
						<li>
							<input type="button"  id= "tangming"  runat="server"
								onclick="javascript:jump('Yingxiao_Teshu_nanguang_Tangming_SpecialVehiclesClassifiedStatistics_i.raq','a')"
								value="塘明站" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"/>
						</li>
					
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						收费站车流路费及相关营运数据统计月报表
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" id="b1" runat="server"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_MarketingWorkPlan_i.raq','a')"
								value="收费站营销工作计划" style="font-family: 微软雅黑;cursor: hand; width: 210px;"/>
						</li>
						<!--
						<li>
							<input type="button" id="b2" runat="server"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Cheliu_i.raq','a')"
								value="收费站车流量统计月报" style="font-family: 微软雅黑;cursor: hand;width: 210px;"/>
						</li>
						-->
						<li>
							<input type="button" id="b3" runat="server"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Cheliuliang_i.raq','a')"
								value="收费站车流量及路费收入统计月报" style="font-family: 微软雅黑;cursor: hand;width: 210px;" />
						</li>
						<li>&nbsp</li>
						<li>
							&nbsp;</li>
					    
					</ul>
					<%--
					</br>
					</br>
					<ul>
					<li>
							<input type="button" 
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Gaofeng_i.raq','<%=station%>')"
								value="收费站高峰应急统计月报" style="font-family: 微软雅黑;color: #000000; width: 210px;cursor: hand;" /></li>
					</ul>
					--%>
				</div>
			</div>
			</div>
	</body>
</html>
