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
            var context = navigation.canvas.context;
            for (var i = 0; i < navigation.states.length; i++) {
                var state = navigation.states[i];
                context.rect(state.x, state.y, state.w, state.h);
            }
            for (var i = 0; i < navigation.transitions.length; i++) {
                var transition = navigation.transitions[i];
                context.moveTo(transition.x1, transition.y);
                context.lineTo(transition.x1, transition.y + transition.h);
                context.lineTo(transition.x2, transition.y + transition.h);
                context.lineTo(transition.x2, transition.y);
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
