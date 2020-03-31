<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineView.aspx.cs" Inherits="HNFactoryAutoSystem.Web.LineView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no" />
    <title>生产线显示</title>

    <link href="Scripts/jsPlumb/css/jsplumbtoolkit-defaults.css" rel="stylesheet" />
    <link href="Scripts/jsPlumb/css/main.css" rel="stylesheet" />
    <link href="Scripts/jsPlumb/css/jsplumbtoolkit-flowchart.css" rel="stylesheet" />
    <link href="Scripts/jsPlumb/css/flowchart.css" rel="stylesheet" />
    <link href="Scripts/jsPlumb/css/flowchartwindow.css" rel="stylesheet" />
    <link href="Scripts/jsPlumb/css/flowchartpump.css" rel="stylesheet" />

    <script src="Scripts/jsPlumb/jsplumb.min.js"></script>
    <script src="Scripts/jsPlumb/flowchart.js"></script>

    <!--Ace Begin-->
    <link href="Scripts/ace_admin_cn/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/ace_admin_cn/assets/css/font-awesome.min.css" rel="stylesheet" />
    <script src="Scripts/ace_admin_cn/assets/js/jquery.min.js"></script>

    <script src="Scripts/bootstrap-table/bootstrap-table.js"></script>
    <link href="Scripts/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-table/locale/bootstrap-table-zh-CN.js"></script>

    <%--<link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace.min.css" />--%>
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/jquery-ui-1.10.3.custom.min.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/chosen.css" />
    <link rel="stylesheet" href="Scripts/ace_admin_cn/assets/css/jquery.gritter.css" />

    <script src="Scripts/ace_admin_cn/assets/js/ace-extra.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/bootstrap.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/typeahead-bs2.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.slimscroll.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.easy-pie-chart.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/jquery.sparkline.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.pie.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/flot/jquery.flot.resize.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/bootbox.min.js"></script>
    <script src="Scripts/ace_admin_cn/assets/js/chosen.jquery.min.js"></script>
    <!--End Ace -->

    <style type="text/css">
        .widget-main {
            padding: 10px
        }

        .list-inline device {
            margin: 0px;
            padding: 0px
        }
        /*粉料桶*/
        .ganfenImage {
            background-image: url(img/fentong.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*水桶*/
        .waterImage {
            background-image: url(img/waterbox.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*热水桶*/
        .hotwaterImage {
            background-image: url(img/hotwaterbox.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*搅拌釜*/
        .jiaobanImage {
            background-image: url(img/jiaoban.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*斜板沉淀池*/
        .chendianImage {
            background-image: url(img/chendian.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*液体储存桶*/
        .liquidboxImage {
            background-image: url(img/liquidbox.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
        /*粉料储存桶*/
        .fenliaoImage {
            background-image: url(img/fenliao.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            -moz-background-size: 100% 100%;
        }
    </style>

    <script>

        $(document).ready(function () {
            $().ready(function () {
                LoadLogList();
                //margin:0px;padding-bottom:5px; padding-top:5px
                //style="padding:9px"
                $(".list-inline").css("margin", "0px");
                $(".list-inline").css("padding-bottom", "5px");
                $(".list-inline").css("padding-top", "5px");
                $(".widget-main").css("padding", "9px");

                $(".pump,.sc,.valve,.mot").click(function () {
                    var pitem = $(this)[0];
                    var pid = pitem.id;
                    SelDeviceConnect(pid);
                });
            });
        });


        function LoadLogList() {
            //加载日志数据
            var lineId = "G01-PL-01";
            $.ajax({
                url: '<%= ResolveUrl("~/Ashx/FactoryProduceHandler.ashx") %>',
                dataType: "json",
                type: "POST",
                data: {
                    Action: "GetFactoryLineProduceLog",
                    LineId: lineId
                },
                success: function (result) {
                    if (result.code == 0) {
                        var items = result.data;
                        //读取数据
                        RefreshSensorStatus(items);
                    } else {
                        showMsg(result.message, function () { });
                    }
                }
            });
        }
        function RefreshSensorStatus(items) {
            //刷新传感器日志状态
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (item.ValueType == "ValveStatus") {
                    ValveUpdate(item);
                }
                else if (item.ValueType == "PumpStatus") {
                    PumpUpdate(item);
                }
                else {
                    var spTitle = "#sp_" + item.SensorId;
                    var spIdTitle = "#sp_" + item.SensorId + "_Id";
                    if ($(spTitle).length > 0) {
                        var text = item.ParValue + "&nbsp;" + item.ParUnit + "&nbsp;&nbsp;";
                        $(spTitle).html(text);

                        var idtext = "<font color='blue'>[" + item.SensorId + "]</font>";
                        $(spIdTitle).html(idtext);
                    }
                }
            }
        }
        function ValveUpdate(item) {
            //阀门
            var valveTitle = "#dv" + item.SensorId;
            //var vRemoveClass = "";
            var vAddClass = "";
            switch (item.SensorStatusValue) {
                case 1:
                    //阀门关闭
                    vAddClass = "valvedivclose";
                    break;
                case 2:
                case 3:
                    //阀门打开
                    vAddClass = "valvedivopen";
                    break;
                case 4:
                    //阀门故障
                    vAddClass = "valvedivbug";
                    break;
                default:
                    //无状态
                    vAddClass = "valvedivnone";
                    break;
            }
            $(valveTitle).removeClass();
            $(valveTitle).addClass(vAddClass);

        }
        function PumpUpdate(item) {
            //泵
            var pumpTitle = "#dv" + item.SensorId;
            var vPumpAddClass = "";
            switch (item.SensorStatusValue) {
                case 1:
                    //泵停止
                    vPumpAddClass = "pumpdivstop";
                    break;
                case 2:
                    vPumpAddClass = "pumpdivrun";
                    //泵运行
                    break;
                case 3:
                    vPumpAddClass = "pumpdivbug";
                    //泵故障
                    break;
                default:
                    //无状态
                    vPumpAddClass = "pumpdivnone";
                    break;
            }
            $(pumpTitle).removeClass();
            $(pumpTitle).addClass(vPumpAddClass);
        }
    </script>
</head>
<body data-demo-id="flowchart">
    <form id="form1" runat="server">
        <div class="jtk-assline-main">
            <div class="jtk-assline-canvas canvas-wide flowchart-assline jtk-surface jtk-surface-nopan" id="canvas">
                <!--工艺环节一设备-->
                <div id="flowchartWindow1" class="window jtk-node ganfenImage">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">1#-1 干粉桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0111">12350KG</span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0111_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="flowchartWindow9" class="window jtk-node waterImage">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">9#净水桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node jiaobanImage" id="flowchartWindow2" style="left: 23em; top: 20.4em; height: 200px; width: 180px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">2#-1 搅拌浸出釜</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main" style="padding: 10px">
                                <ul class="list-inline" style="margin-bottom: 3px">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0211" style=""></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0211_Id"></span>
                                    </li>
                                </ul>
                                <ul class="list-inline" style="margin-bottom: 3px">
                                    <li>
                                        <i class="icon-bar-chart  green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_T0212"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_T0212_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node chendianImage" id="flowchartWindow3" style="left: 64em; top: 48em; height: 100px; width: 200px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">03#-1 斜板沉淀池</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node liquidboxImage" id="flowchartWindow4" style="left: 95em; top:2em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">4#-1 浸出液储存桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0411"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0411_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node liquidboxImage" id="flowchartWindow5_1" style="left: 61em; top: 2.4em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">5#-1 浸出剂桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0511"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0511_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node liquidboxImage" id="flowchartWindow5_2" style="left: 74em; top: 2.4em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">5#-2 浸出剂桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0515"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0515_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="window jtk-node hotwaterImage" id="flowchartWindow7" style="left: 23em; top: 40em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">7# 热水桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W0711"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W0711_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="flowchartPump0912" class="pump jtk-node" style="left: 5.6em; top: 60em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0912">
                        </div>
                        <div>
                            Pump0912
                        </div>
                    </div>
                </div>
                <div id="flowchartV0916" class="valve jtk-node" style="left: 17.8em; top: 60em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV0916">
                        </div>
                        <div>
                            V0916
                        </div>
                    </div>
                </div>
                <div id="flowchartSC0111" class="sc jtk-node" style="left: 5.6em; top: 20.4em">
                    <div class="widget-box windowbox">
                        <div class="scdivstop" id="dvSC0111">
                        </div>
                        <div>
                            SC0111
                        </div>
                    </div>
                </div>
                <div id="flowchartPump0911" class="pump jtk-node" style="left: 5.6em; top: 29.6em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0911">
                        </div>
                        <div>
                            Pump0911
                        </div>
                    </div>
                </div>
                <div id="flowchartPump0711" class="pump jtk-node" style="left: 43em; top: 60em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0711">
                        </div>
                        <div>
                            Pump0711
                        </div>
                    </div>
                </div>
                <div id="flowchartV0717" class="valve jtk-node" style="left: 30.3em; top: 60em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV0717">
                        </div>
                        <div>
                            V0717
                        </div>
                    </div>
                </div>
                <div id="flowchartV0215" class="valve jtk-node" style="left: 43em; top: 25.8em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV0215">
                        </div>
                        <div>
                            V0215
                        </div>
                    </div>
                </div>
                <div id="flowchartMOT0210" class="mot jtk-node" style="left: 27.8em; top: 6em">

                    <div class="widget-box windowbox">
                        <div>
                            <ul class="list-unstyled spaced" style="margin-bottom: 0px">
                                <li style="text-align: center; margin-bottom: 0px">
                                    <i class="icon-cogs  green"></i>
                                    <span id="sp_MOT0210-1"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="motdivstop" id="dvMOT0210">
                        </div>
                        <div>
                            MOT0210
                        </div>
                    </div>
                </div>
                <div id="flowchartV0217" class="valve jtk-node" style="left: 53em; top: 37.2em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV0217">
                        </div>
                        <div>
                            V0217
                        </div>
                    </div>
                </div>
                <div id="flowchartPump0211" class="pump jtk-node" style="left: 53em; top: 49.4em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0211">
                        </div>
                        <div>
                            Pump0211
                        </div>
                    </div>
                </div>
                <div id="flowchartMOT0311" class="mot jtk-node" style="left: 69.5em; top: 33em">

                    <div class="widget-box windowbox">
                        <div>
                            <ul class="list-unstyled spaced" style="margin-bottom: 0px">
                                <li style="text-align: center; margin-bottom: 0px">
                                    <i class="icon-cogs  green"></i>
                                    <span id="sp_MOT0311"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="motdivstop" id="dvMOT0311">
                        </div>
                        <div>
                            MOT0311
                        </div>
                    </div>

                </div>
                <div id="flowchartPump0511" class="pump jtk-node" style="left: 56em; top: 20em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0511">
                        </div>
                        <div>
                            Pump0511
                        </div>
                    </div>
                </div>
                <div id="flowchartPump0512" class="pump jtk-node" style="left: 56em; top: 27.4em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0512">
                        </div>
                        <div>
                            Pump0512
                        </div>
                    </div>
                </div>
                <div id="flowchartV0214" class="valve jtk-node" style="left: 40em; top: 14.2em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV0214">
                        </div>
                        <div>
                            V0214
                        </div>
                    </div>
                </div>
                <!--工艺环节一设备-->

                <!--工艺环节二设备-->
                <div class="window jtk-node jiaobanImage" id="flowchartWindow10" style="left: 124.6em; top: 30.6em; height: 200px; width: 180px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">10#-1 搅拌反应釜</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline" style="margin: 0px; padding-bottom: 5px; padding-top: 5px">
                                    <li>
                                        <i class="icon-download  green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W1011" style=""></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W1011_Id"></span>
                                    </li>
                                </ul>
                                <ul class="list-inline" style="margin: 0px; padding-bottom: 5px; padding-top: 5px">
                                    <li>
                                        <i class="icon-bar-chart  green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_T1012"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_T1012_Id"></span>
                                    </li>
                                </ul>
                                <ul class="list-inline" style="margin: 0px; padding-bottom: 5px; padding-top: 5px">
                                    <li>
                                        <i class="icon-beaker green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_PH1013"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_PH1013_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="flowchartWindow12" class="window jtk-node fenliaoImage" style="left: 88.4em; top: 22.8em; height: 140px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">12#-1 粉料</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W1211"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W1211_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="flowchartWindow14" class="window jtk-node fenliaoImage" style="left: 88.4em; top: 35.8em; height: 140px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">14#-1 粉料</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W1411"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W1411_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="flowchartWindow32" class="window jtk-node fenliaoImage" style="left: 88.4em; top: 48.2em; height: 140px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">32#-1 粉料</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W3211"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W3211_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="flowchartPump0411" class="pump jtk-node" style="left: 122.2em; top: 6.5em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump0411">
                        </div>
                        <div>
                            Pump0411
                        </div>
                    </div>
                </div>
                <div id="flowchartSC1211" class="sc jtk-node" style="left: 109.1em; top: 25.76em">
                    <div class="widget-box windowbox">
                        <div class="scdivstop" id="dvSC1211">
                        </div>
                        <div>
                            SC1211
                        </div>
                    </div>
                </div>
                <div id="flowchartSC1411" class="sc jtk-node" style="left: 109.1em; top: 38.8em">
                    <div class="widget-box windowbox">
                        <div class="scdivstop" id="dvSC1411">
                        </div>
                        <div>
                            SC1411
                        </div>
                    </div>
                </div>
                <div id="flowchartSC3211" class="sc jtk-node" style="left: 109.1em; top: 51.2em">
                    <div class="widget-box windowbox">
                        <div class="scdivstop" id="dvSC3211">
                        </div>
                        <div>
                            SC3211
                        </div>
                    </div>
                </div>

                <div id="flowchartMOT1010" class="mot jtk-node" style="left: 129.4em; top: 17.4em">

                    <div class="widget-box windowbox">
                        <div>
                            <ul class="list-unstyled spaced" style="margin-bottom: 0px">
                                <li style="text-align: center; margin-bottom: 0px">
                                    <i class="icon-cogs  green"></i>
                                    <span id="sp_MOT1010-1"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="motdivstop" id="dvMOT1010">
                        </div>
                        <div>
                            MOT1010
                        </div>
                    </div>

                </div>
                <div id="flowchartV1015" class="valve jtk-node" style="left: 122.2em; top: 17.8em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV1015">
                        </div>
                        <div>
                            V1015
                        </div>
                    </div>
                </div>

                <div class="window jtk-node liquidboxImage" id="flowchartWindow11_1" style="left: 147.4em; top: 2.4em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">11#-1 氨水桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>
                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_L1116"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_L1116_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node liquidboxImage" id="flowchartWindow11_3" style="left: 160em; top: 2.4em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">11#-3 氨水桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_L1136"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_L1136_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>

                <div id="flowchartPump1111" class="pump jtk-node" style="left: 148em; top: 24.6em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump1111">
                        </div>
                        <div>
                            Pump1111
                        </div>
                    </div>
                </div>
                <div id="flowchartV1014" class="valve jtk-node" style="left: 139.5em; top: 14.2em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV1014">
                        </div>
                        <div>
                            V1014
                        </div>
                    </div>
                </div>
                <div id="flowchartMOT1511" class="mot jtk-node" style="left: 155.5em; top: 21em">

                    <div class="widget-box windowbox">
                        <div>
                            <ul class="list-unstyled spaced" style="margin-bottom: 0px">
                                <li style="text-align: center; margin-bottom: 0px">
                                    <i class="icon-cogs  green"></i>
                                    <span id="sp_MOT1511"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="motdivstop" id="dvMOT1511">
                        </div>
                        <div>
                            MOT1511
                        </div>
                    </div>

                </div>
                <div class="window jtk-node chendianImage" id="flowchartWindow15" style="left: 150em; top: 34em; height: 100px; width: 200px">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">15#-1 斜板沉淀池</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="window jtk-node liquidboxImage" id="flowchartWindow16" style="left: 160em; top: 45.8em">
                    <div class="widget-box windowbox">
                        <div class="widget-header header-color-blue">
                            <h6 class="smaller lighter">16#-1 净化液储存桶</h6>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <ul class="list-inline">
                                    <li>
                                        <i class="icon-download  green"></i>

                                    </li>
                                    <li style="width: 30%; text-align: center">
                                        <span id="sp_W1611"></span>
                                    </li>
                                    <li style="width: 40%">
                                        <span style="color: blue" id="sp_W1611_Id"></span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="flowchartPump1011" class="pump jtk-node" style="left: 142.1em; top: 54.6em">
                    <div class="widget-box windowbox">
                        <div class="pumpdivstop" id="dvPump1011">
                        </div>
                        <div>
                            Pump1011
                        </div>
                    </div>
                </div>
                <div id="flowchartV1016" class="valve jtk-node" style="left: 129.4em; top: 54.6em">
                    <div class="widget-box windowbox">
                        <div class="valvedivclose" id="dvV1016">
                        </div>
                        <div>
                            V1016
                        </div>
                    </div>
                </div>

                <!--工艺环节二设备-->
            </div>
        </div>
    </form>
</body>
</html>
