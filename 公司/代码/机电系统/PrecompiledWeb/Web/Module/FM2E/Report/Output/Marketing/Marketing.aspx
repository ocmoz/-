<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Output_Marketing, App_Web_7j2xmizs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

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
			<div id="table">
				<div id="ptk">
					<div id="tabtop-z">
						直管路段车流量、路费收入分析汇总表
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 100px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_zonghejieguo_TrafficVolumeIncomeStatistical_o_o.raq','')"
								value="综合结果" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 100px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_sumdongmi_TrafficVolumeIncomeStatistical_o.raq','')"
								value="报送董秘处" />
						</li>
						</ul>
						<br />
						<br />
						<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_meiguan_TrafficVolumeIncomeStatistical_o.raq','')"
								value="梅观" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_jihedong_TrafficVolumeIncomeStatistical_o.raq','')"
								value="机荷东" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_yanba_TrafficVolumeIncomeStatistical_o.raq','')"
								value="盐坝" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_jihexi_TrafficVolumeIncomeStatistical_o.raq','')"
								value="机荷西" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_yanpai_TrafficVolumeIncomeStatistical_o.raq','')"
								value="盐排" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Zhiguan_nanguang_TrafficVolumeIncomeStatistical_o.raq','')"
								value="南光" />
						</li>
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-z">
						深圳区域路网特殊车辆分类统计
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 100px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_sum_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="汇总" />
						</li>
						</ul>
						<br />
						<br />
						<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_meiguan_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="梅观" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_jihedong_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="机荷东" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_yanba_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="盐坝" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_jihexi_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="机荷西" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_yanpai_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="盐排" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 68px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Teshu_nanguang_SpecialVehiclesClassifiedStatistics_o.raq','')"
								value="南光" />
						</li>
						
					</ul>
				</div>
			</div>
			<div id="table">
				<div id="ptk">
					<div id="tabtop-z">
						收费站车流路费及相关营运数据统计月报表
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_MarketingWorkPlan_o.raq','')"
								value="收费站营销工作计划" />
						</li>
						<%-- 
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Cheliu_o.raq','')"
								value="收费站车流量统计月报" />
						</li>
						--%>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 252px; cursor: hand;"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Cheliuliang_o.raq','')"
								value="收费站车流量统计及路费收入统计月报" />
						</li>
						</ul>
						<%-- 
						<br />
						<br />
						<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑; height: 30px; width: 200px;cursor: hand;"
								onclick="javascript:jump('Yingxiao_Shoufeizhan_Gaofeng_o.raq','')"
								value="高峰应急统计月报" />
						</li>
					</ul>
					--%>
				</div>
			</div>
			</div>
	</body>
</html>

