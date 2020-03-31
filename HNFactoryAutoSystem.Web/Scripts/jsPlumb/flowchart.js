var instance;

//设备启动后连线的样式
var derviceSelectConnectorStyle = {
        strokeWidth: 4,
        stroke: "blue",
        outlineWidth: 5,
        outlineStroke: "white"
    }
jsPlumb.ready(function () {

    instance = window.jsp = jsPlumb.getInstance({
        // default drag options
        DragOptions: { cursor: 'pointer', zIndex: 2000 },
        // the overlays to decorate each connection with.  note that the label overlay uses a function to generate the label text; in this
        // case it returns the 'labelText' member that we set on each connection in the 'init' method below.
        ConnectionOverlays: [
            ["Arrow", {
                location: 1,
                visible: true,
                width: 11,
                length: 11,
                id: "ARROW",
                events: {
                    click: function () { alert("you clicked on the arrow overlay"); }
                }
            }],
            ["Label", {
                location: 0.1,
                id: "label",
                cssClass: "aLabel",
                events: {
                    tap: function () { alert("hey"); }
                }
            }]
        ],
        Container: "canvas"
    });

    var basicType = {
        connector: "StateMachine",
        paintStyle: { stroke: "red", strokeWidth: 4 },
        hoverPaintStyle: { stroke: "blue" },
        overlays: [
            "Arrow"
        ]
    };

    //instance.registerConnectionType("derviceSelect", derviceSelectConnectorStyle);
    instance.registerConnectionTypes({
        "basic": {
            paintStyle: { stroke: "#61B7CF", strokeWidth: 2 }
        },
        "derviceSelect": {
            paintStyle: { stroke: "red", strokeWidth: 5 }
        }
    });

    // this is the paint style for the connecting lines..
    var connectorPaintStyle = {
        strokeWidth: 2,
        stroke: "#61B7CF",
        joinstyle: "round",
        outlineStroke: "white",
        outlineWidth: 2
    },
        // .. and this is the hover style.
        connectorHoverStyle = {
            strokeWidth: 3,
            stroke: "#216477",
            outlineWidth: 5,
            outlineStroke: "white"
        },
        endpointHoverStyle = {
            fill: "#216477",
            stroke: "#216477"
        },
        // the definition of source endpoints (the small blue ones)
        sourceEndpoint = {
            endpoint: "Dot",
            paintStyle: {
                stroke: "#7AB02C",
                fill: "transparent",
                radius: 5,
                strokeWidth: 1
            },
            isSource: true,
            connector: ["Flowchart", { stub: [40, 60], gap: 10, cornerRadius: 5, alwaysRespectStubs: true }],
            connectorStyle: connectorPaintStyle,
            hoverPaintStyle: endpointHoverStyle,
            connectorHoverStyle: connectorHoverStyle,
            dragOptions: {},
            overlays: [
                ["Label", {
                    location: [0.5, 1.5],
                    label: "Drag",
                    cssClass: "endpointSourceLabel",
                    visible: false
                }]
            ]
        },
        // the definition of target endpoints (will appear when the user drags a connection)
        targetEndpoint = {
            endpoint: "Dot",
            paintStyle: { fill: "#7AB02C", radius: 5 },
            hoverPaintStyle: endpointHoverStyle,
            maxConnections: -1,
            dropOptions: { hoverClass: "hover", activeClass: "active" },
            isTarget: true,
            overlays: [
                ["Label", { location: [0.5, -0.5], label: "Drop", cssClass: "endpointTargetLabel", visible: false }]
            ]
        },
        init = function (connection) {
            //连接线显示的标签
            //connection.getOverlay("label").setLabel(connection.sourceId.substring(15) + "-" + connection.targetId.substring(15));
        };

    var _addEndpoints = function (toId, targetAnchors, sourceAnchors) {
        for (var i = 0; i < sourceAnchors.length; i++) {
            var sourceUUID = toId + sourceAnchors[i];
            instance.addEndpoint("flowchart" + toId, sourceEndpoint, {
                anchor: sourceAnchors[i], uuid: sourceUUID
            });
        }
        for (var j = 0; j < targetAnchors.length; j++) {
            var targetUUID = toId + targetAnchors[j];
            instance.addEndpoint("flowchart" + toId, targetEndpoint, { anchor: targetAnchors[j], uuid: targetUUID });
        }
    };

    // suspend drawing and initialise.
    instance.batch(function () {

        //_addEndpoints("Window4", ["TopCenter", "BottomCenter", "TopLeft"], ["LeftMiddle", "RightMiddle"]);
        //_addEndpoints("Window2", ["LeftMiddle", "BottomCenter"], ["TopCenter", "RightMiddle"]);
        //_addEndpoints("Window3", ["RightMiddle", "BottomCenter"], ["LeftMiddle", "TopCenter"]);
        //_addEndpoints("Window1", ["LeftMiddle", "RightMiddle"], ["TopCenter", "BottomCenter"]);

        
        CreateOneDevice(_addEndpoints);
        CreateTwoDevice(_addEndpoints);
        CreateThreeDevice(_addEndpoints);
        // listen for new connections; initialise them the same way we initialise the connections at startup.
        instance.bind("connection", function (connInfo, originalEvent) {
            init(connInfo.connection);
        });

        // make all the window divs draggable
        instance.draggable(jsPlumb.getSelector(".flowchart-assline .window"), { grid: [20, 20] });
        // THIS DEMO ONLY USES getSelector FOR CONVENIENCE. Use your library's appropriate selector
        // method, or document.querySelectorAll:
        jsPlumb.draggable(document.querySelectorAll(".window"), { grid: [20, 20] });

        // connect a few up
        //关系连线 LoadConnect(instance, "源设备编号", "源设备出口", "目标设备编号", "目标设备接口");
        
        OneLinkDeviceConnect(instance);
        TwoLinkDeviceConnect(instance);
        ThreeLinkDeviceConnect(instance);
        //instance.connect({ uuids: ["Window5_1BottomCenter", "Window2TopRight"] });
        //instance.connect({ uuids: ["Window2BottomRight", "Window3LeftMiddle"] });
        //instance.connect({ uuids: ["Window3RightMiddle", "Window4LeftMiddle"] });


        //
        // listen for clicks on connections, and offer to delete connections on click.
        //
        instance.bind("click", function (conn, originalEvent) {
            //if (confirm("Delete connection from " + conn.sourceId + " to " + conn.targetId + "?"))
            //   instance.detach(conn);
            //conn.toggleType("basic");
        });

        instance.bind("connectionDrag", function (connection) {
            console.log("connection " + connection.id + " is being dragged. suspendedElement is ", connection.suspendedElement, " of type ", connection.suspendedElementType);
        });

        instance.bind("connectionDragStop", function (connection) {
            console.log("connection " + connection.id + " was dragged");
        });

        instance.bind("connectionMoved", function (params) {
            console.log("connection " + params.connection.id + " was moved");
        });
    });

    jsPlumb.fire("jsPlumbDemoLoaded", instance);

});
/**
 * 对第一环节的设备进行连接
 * @param {jsPlumb.getInstance} instance 环节连接器
 */
function OneLinkDeviceConnect(instance) {
    //干粉桶到泵到搅拌浸出釜
    LoadConnect(instance, "Window1", "BottomCenter", "SC0111", "TopCenter");
    LoadConnect(instance, "SC0111", "RightMiddle", "Window2", "TopLeft");
    //净水桶到泵到搅拌浸出釜
    LoadConnect(instance, "Window9", "TopCenter", "Pump0911", "BottomCenter");
    LoadConnect(instance, "Pump0911", "RightMiddle", "Window2", "LeftMiddle");
    //净水桶到泵到阀门到热水桶
    LoadConnect(instance, "Window9", "BottomCenter", "Pump0912", "TopCenter");
    LoadConnect(instance, "Pump0912", "RightMiddle", "V0916", "LeftMiddle");
    LoadConnect(instance, "V0916", "RightMiddle", "Window7", "LeftMiddle");
    //热水桶到阀门到泵到阀门到搅拌浸出釜
    LoadConnect(instance, "Window7", "BottomCenter", "V0717", "TopCenter");
    LoadConnect(instance, "V0717", "RightMiddle", "Pump0711", "LeftMiddle");
    LoadConnect(instance, "Pump0711", "TopCenter", "V0215", "BottomCenter");
    LoadConnect(instance, "V0215", "LeftMiddle", "Window2", "RightMiddle");
    //搅拌电机到搅拌浸出釜
    LoadConnect(instance, "MOT0210", "BottomCenter", "Window2", "TopCenter");
    //搅拌浸出釜到阀门到泵到斜板沉淀池
    LoadConnect(instance, "Window2", "BottomRight", "V0217", "LeftMiddle");
    LoadConnect(instance, "V0217", "BottomCenter", "Pump0211", "TopCenter");
    LoadConnect(instance, "Pump0211", "RightMiddle", "Window3", "LeftMiddle");
    //搅拌电机到斜板沉淀池
    LoadConnect(instance, "MOT0311", "BottomCenter", "Window3", "TopCenter");
    //浸出剂桶到泵到阀门到搅拌浸出釜
    LoadConnect(instance, "Window5_1", "BottomCenter", "Pump0511", "RightMiddle");
    LoadConnect(instance, "Window5_1", "BottomRight", "Pump0512", "RightMiddle");
    LoadConnect(instance, "Window5_2", "BottomCenter", "Pump0511", "RightMiddle");
    LoadConnect(instance, "Window5_2", "BottomRight", "Pump0512", "RightMiddle");
    LoadConnect(instance, "Pump0511", "LeftMiddle", "V0214", "RightMiddle");
    LoadConnect(instance, "Pump0512", "LeftMiddle", "V0214", "RightMiddle");
    LoadConnect(instance, "V0214", "TopCenter", "Window2", "TopRight");
    //斜板沉淀池到浸出液存储桶
    LoadConnect(instance, "Window3", "RightMiddle", "Window4", "LeftMiddle");
}
/**
 * 对第二环节的设备进行连接
 * @param {jsPlumb.getInstance} instance 环节连接器
 */
function TwoLinkDeviceConnect(instance) {
    //浸出液存储桶到泵到搅拌反应釜
    LoadConnect(instance, "Window4", "RightMiddle", "Pump0411", "LeftMiddle");
    LoadConnect(instance, "Pump0411", "BottomCenter", "V1015", "TopCenter");
    LoadConnect(instance, "V1015", "BottomCenter", "Window10", "TopLeft");
    //粉料桶12到泵到搅拌反应釜
    LoadConnect(instance, "Window12", "RightMiddle", "SC1211", "LeftMiddle");
    LoadConnect(instance, "SC1211", "RightMiddle", "Window10", "LeftMiddle");
    //粉料桶14到泵到搅拌反应釜
    LoadConnect(instance, "Window14", "RightMiddle", "SC1411", "LeftMiddle");
    LoadConnect(instance, "SC1411", "RightMiddle", "Window10", "LeftMiddle");
    //粉料桶32到泵到搅拌反应釜
    LoadConnect(instance, "Window32", "RightMiddle", "SC3211", "LeftMiddle");
    LoadConnect(instance, "SC3211", "RightMiddle", "Window10", "LeftMiddle");
    //搅拌电机到搅拌反应釜
    LoadConnect(instance, "MOT1010", "BottomCenter", "Window10", "TopCenter");
    //氨水桶到泵到阀门到搅拌反应釜
    LoadConnect(instance, "Window11_1", "BottomCenter", "Pump1111", "TopCenter");
    LoadConnect(instance, "Pump1111", "BottomCenter", "V1014", "TopCenter");
    LoadConnect(instance, "V1014", "BottomCenter", "Window10", "TopRight");
    //搅拌反应釜到阀门到泵到斜板沉淀池
    LoadConnect(instance, "Window10", "BottomCenter", "V1016", "TopCenter");
    LoadConnect(instance, "V1016", "RightMiddle", "Pump1011", "LeftMiddle");
    LoadConnect(instance, "Pump1011", "TopCenter", "Window15", "LeftMiddle");
    //搅拌电机到斜板沉淀池
    LoadConnect(instance, "MOT1511", "BottomCenter", "Window15", "TopCenter");
    //斜板沉淀池到净化液储存罐
    LoadConnect(instance, "Window15", "RightMiddle", "Window16", "TopCenter");
}
/**
 * 对第三环节的设备进行连接
 * @param {jsPlumb.getInstance} instance 环节连接器
 */
function ThreeLinkDeviceConnect(instance) {

}

/**
 * 第一生产环节设备初始化
 * @param {object} _addEndpoints 设备构建器
 */
function CreateOneDevice(_addEndpoints) {
    //干粉桶
    _addEndpoints("Window1", ["TopCenter"], ["BottomCenter"]);
    //搅拌浸出釜
    _addEndpoints("Window2", ["LeftMiddle", "RightMiddle", "TopLeft", "TopCenter", "TopRight"], ["BottomRight"]);
    //斜板沉淀池
    _addEndpoints("Window3", ["LeftMiddle", "TopCenter"], ["RightMiddle", "BottomCenter"]);
    //浸出液存储桶
    _addEndpoints("Window4", ["LeftMiddle"], ["RightMiddle"]);
    //浸出剂桶1/2
    _addEndpoints("Window5_1", ["TopCenter"], ["BottomCenter", "BottomRight"]);
    _addEndpoints("Window5_2", ["TopCenter"], ["BottomCenter", "BottomRight"]);
    //净水桶
    _addEndpoints("Window9", ["LeftMiddle"], ["TopCenter", "BottomCenter"]);
    //热水桶
    _addEndpoints("Window7", ["LeftMiddle"], ["BottomCenter"]);
    //泵和阀门设置
    _addEndpoints("SC0111", ["TopCenter"], ["RightMiddle"]);
    _addEndpoints("Pump0911", ["BottomCenter"], ["RightMiddle"]);
    _addEndpoints("Pump0912", ["TopCenter"], ["RightMiddle"]);
    _addEndpoints("V0916", ["LeftMiddle"], ["RightMiddle"]);
    _addEndpoints("V0717", ["TopCenter"], ["RightMiddle"]);
    _addEndpoints("Pump0711", ["LeftMiddle"], ["TopCenter"]);
    _addEndpoints("V0215", ["BottomCenter"], ["LeftMiddle"]);
    _addEndpoints("MOT0210", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("V0217", ["LeftMiddle"], ["BottomCenter"]);
    _addEndpoints("Pump0211", ["TopCenter"], ["RightMiddle"]);
    _addEndpoints("MOT0311", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("Pump0511", ["RightMiddle"], ["LeftMiddle"]);
    _addEndpoints("Pump0512", ["RightMiddle"], ["LeftMiddle"]);
    _addEndpoints("V0214", ["RightMiddle"], ["TopCenter"]);
}
/**
 * 第二生产环节设备初始化
 * @param {object} _addEndpoints 设备构建器
 */
function CreateTwoDevice(_addEndpoints) {
    //粉料桶12
    _addEndpoints("Window12", ["LeftMiddle"], ["RightMiddle"]);
    //粉料桶14
    _addEndpoints("Window14", ["LeftMiddle"], ["RightMiddle"]);
    //粉料桶32
    _addEndpoints("Window32", ["LeftMiddle"], ["RightMiddle"]);
    //搅拌反应釜
    _addEndpoints("Window10", ["LeftMiddle", "TopLeft", "TopCenter", "TopRight"], ["BottomCenter"]);
    //氨水桶1-3
    _addEndpoints("Window11_1", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("Window11_3", ["TopCenter"], ["BottomCenter"]);
    //斜板沉淀池
    _addEndpoints("Window15", ["LeftMiddle", "TopCenter"], ["RightMiddle", "BottomCenter"]);
    //净化液储存桶
    _addEndpoints("Window16", ["TopCenter","LeftMiddle"], ["RightMiddle"]);

    //泵和阀门
    _addEndpoints("Pump0411", ["LeftMiddle"], ["BottomCenter"]);
    _addEndpoints("SC1211", ["LeftMiddle"], ["RightMiddle"]);
    _addEndpoints("SC1411", ["LeftMiddle"], ["RightMiddle"]);
    _addEndpoints("SC3211", ["LeftMiddle"], ["RightMiddle"]);
    _addEndpoints("MOT1010", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("V1015", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("Pump1111", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("V1014", ["TopCenter"], ["BottomCenter"]);
    _addEndpoints("Pump1011", ["LeftMiddle"], ["TopCenter"]);
    _addEndpoints("V1016", ["TopCenter"], ["RightMiddle"]);
    _addEndpoints("MOT1511", ["TopCenter"], ["BottomCenter"]);
}
/**
 * 第三生产环节设备初始化
 * @param {object} _addEndpoints 设备构建器
 */
function CreateThreeDevice(_addEndpoints) {

}


/**
 * 对设备进行连接
 * @param {Object} instance 连接对象
 * @param {String} sourceId 源设备编号
 * @param {String} sourceConn 源设备接口
 * @param {String} targetId 目标设备编号
 * @param {String} targetConn 目标设备接口
 */
function LoadConnect(instance, sourceId, sourceConn, targetId, targetConn) {
    instance.connect({ uuids: [sourceId + sourceConn, targetId + targetConn] });
}

function SelDeviceConnect(deviceId) {
    // 获取JsPlumb对象内所有线数据
    var connections = instance.getAllConnections();
    for (var i in connections) {
        // connections 是线数据数组
        var sourceId = connections[i].sourceId;    // 线的起始html元素的ID
        var targetId = connections[i].targetId;    // 线的目标html元素的ID
        if (sourceId == deviceId || targetId == deviceId) {
            //connections[i].connectorStyle = derviceSelectConnectorStyle;
            connections[i].toggleType("derviceSelect");
        }

        //connections[i].endpoints;    // 线的端点 有起始端点和目标端点 endpoints是一个数组
        //connections[i].endpoints[0].anchor;    // endpoints[0]是起始端点 anchor是锚点
        //connections[i].endpoints[0].anchor.type;    // 锚点的type（类型），位置
        //connections[i].endpoints[1].anchor;        // endpoints[1]是目标端点 anchor是锚点
        //connections[i].endpoints[1].anchor.type;    // 锚点的type（类型），位置
    }
}
function UnSelDeviceConnect(deviceId) {
    // 获取JsPlumb对象内所有线数据
    var connections = instance.getAllConnections();
    for (var i in connections) {
        // connections 是线数据数组
        var sourceId = connections[i].sourceId;    // 线的起始html元素的ID
        var targetId = connections[i].targetId;    // 线的目标html元素的ID
        if (sourceId == deviceId || targetId == deviceId) {
            //connections[i].connectorStyle = derviceSelectConnectorStyle;
            connections[i].toggleType("basic");
        }

        //connections[i].endpoints;    // 线的端点 有起始端点和目标端点 endpoints是一个数组
        //connections[i].endpoints[0].anchor;    // endpoints[0]是起始端点 anchor是锚点
        //connections[i].endpoints[0].anchor.type;    // 锚点的type（类型），位置
        //connections[i].endpoints[1].anchor;        // endpoints[1]是目标端点 anchor是锚点
        //connections[i].endpoints[1].anchor.type;    // 锚点的type（类型），位置
    }
}
