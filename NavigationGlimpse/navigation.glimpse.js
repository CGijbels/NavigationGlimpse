(function ($, pubsub, tab, render) {
    var dialogs = {};
    (function(){
        var render = function () {
        };
        pubsub.subscribe('trigger.navigation.event.render', render);
    })();
    (function () {
        var init = function () {
            pubsub.publish('trigger.navigation.event.render');
        },
        prerender = function (args) {
            dialogs.data = args.pluginData.data;
            args.pluginData.data = 'Loading data, please wait...';
            pubsub.publishAsync('trigger.navigation.init');
        };
        pubsub.subscribe('trigger.navigation.init', init);
        pubsub.subscribe('action.panel.rendering.navigation_glimpse', prerender);
    })();
})(jQueryGlimpse, glimpse.pubsub, glimpse.tab, glimpse.render);
