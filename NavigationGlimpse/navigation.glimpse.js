(function ($, pubsub) {
    var navigation = {};

    (function () {
        var setup = function () {
            navigation.scope.html('<canvas id="navigation-glimpse" width="800px" height="400px"></canvas>');
            navigation.canvas = $('#navigation-glimpse')[0];
            navigation.canvas.context = navigation.canvas.getContext("2d");
        };
        pubsub.subscribe('trigger.navigation.shell.init', setup);
    })();

    (function(){
        var render = function () {
            var font = '12px "Segoe UI Light","Segoe UI Web Regular","Segoe UI","Helvetica Neue",Helvetica,Arial';
            var context = navigation.canvas.context;
            context.save();
            context.fillStyle = '#fff';
            context.shadowOffsetX = 2;
            context.shadowOffsetY = 2;
            context.shadowBlur = 10;
            context.shadowColor = '#999';
            for (var i = 0; i < navigation.states.length; i++) {
                var state = navigation.states[i];
                context.rect(state.x, state.y, state.w, state.h);
                context.fill();
            }
            context.restore();
            context.fillStyle = '#000';
            context.font = 'bold ' + font;
            for (var i = 0; i < navigation.states.length; i++) {
                var state = navigation.states[i];
                var shift = Math.max(0, (state.w - context.measureText(state.key).width) / 2);
                context.fillText(state.key, state.x + shift, state.y + 30, state.w - 2);
            }
            context.font = font;
            for (var i = 0; i < navigation.transitions.length; i++) {
                var transition = navigation.transitions[i];
                context.moveTo(transition.x1, transition.y);
                context.lineTo(transition.x1, transition.y + transition.h);
                context.lineTo(transition.x2, transition.y + transition.h);
                context.lineTo(transition.x2, transition.y);
                var shift = (transition.x2 - transition.x1 - context.measureText(transition.key).width) / 2;
                context.fillText(transition.key, transition.x1 + shift, transition.y + transition.h + 12);
                context.moveTo(transition.x2 - 5, transition.y + 10);
                context.lineTo(transition.x2, transition.y);
                context.lineTo(transition.x2 + 5, transition.y + 10);
            }
            context.stroke();
        };
        pubsub.subscribe('trigger.navigation.event.render', render);
    })();

    (function () {
        var init = function () {
            pubsub.publish('trigger.navigation.shell.init');
            pubsub.publish('trigger.navigation.event.render');
        },
        prerender = function (args) {
            args.pluginData._data = args.pluginData.data;
            args.pluginData.data = 'Loading data, please wait...';
        },
        postrender = function (args) {
            navigation.data = args.pluginData._data;
            navigation.states = navigation.data.item1;
            navigation.transitions = navigation.data.item2;
            navigation.scope = args.panel;
            args.pluginData.data = 'Loading data, please wait...';
            pubsub.publishAsync('trigger.navigation.init');
        };
        pubsub.subscribe('trigger.navigation.init', init);
        pubsub.subscribe('action.panel.rendering.navigation_glimpse', prerender);
        pubsub.subscribe('action.panel.rendered.navigation_glimpse', postrender);
    })();
})(jQueryGlimpse, glimpse.pubsub);
