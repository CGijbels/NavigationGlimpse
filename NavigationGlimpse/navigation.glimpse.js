﻿(function ($, pubsub) {
    var navigation = {};

    (function () {
        var setup = function () {
            navigation.scope.html('<div style="display:table;margin:10px auto"><div style="display:table-row">'
                + '<div style="display:table-row"><canvas id="navigation-glimpse"></canvas>'
                + '</div><div style="display:table-cell; vertical-align:top">'
                + '<div id="navigation-key" class="glimpse-header" style="text-align:center;padding:0"></div>'
                + '<table style="width:320px"><tbody class="glimpse-row-holder"><tr class="glimpse-row">'
                + '<th scope="row" style="width:20%">page</th><td id="navigation-page"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">title</th><td id="navigation-title"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">route</th><td id="navigation-route"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">defaults</th><td id="navigation-defaults"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">defaultTypes</th><td id="navigation-defaultTypes"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">derived</th><td id="navigation-derived"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">trackCrumbTrail</th><td id="navigation-trackCrumbTrail"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">checkPhysicalUrlAccess</th><td id="navigation-checkPhysicalUrlAccess"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">theme</th><td id="navigation-theme"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">masters</th><td id="navigation-masters"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">mobilePage</th><td id="navigation-mobilePage"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">mobileTheme</th><td id="navigation-mobileTheme"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">mobileMasters</th><td id="navigation-mobileMasters"></td></tr>'
                + '<tr class="glimpse-row"><th scope="row">mobileRoute</th><td id="navigation-mobileRoute"></td></tr>'
                + '</tbody></table></div></div></div>');
            navigation.canvas = $('#navigation-glimpse')[0];
            navigation.canvas.width = 750;
            navigation.canvas.height = 400;
            navigation.canvas.context = navigation.canvas.getContext('2d');
            navigation.font = '"Segoe UI Web Regular", "Segoe UI", "Helvetica Neue", Helvetica, Arial';
        };
        pubsub.subscribe('trigger.navigation.shell.init', setup);
    })();

    (function () {
        var wireListeners = function () {
                navigation.scope.delegate('#navigation-glimpse', 'click', function (e) {
                    update(navigation.states, e.offsetX, e.offsetY);
                    render();
                });
            },
            update = function (states, x, y) {
                var oldSelection,
                    newSelection = null;
                for (var i = 0; i < states.length; i++) {
                    var state = states[i];
                    if (state.selected)
                        oldSelection = state;
                    if (state.x <= x && x <= state.x + state.w && state.y <= y && y <= state.y + state.h) {
                        state.selected = true;
                        newSelection = state;
                    }
                }
                if (newSelection && oldSelection && oldSelection !== newSelection)
                    oldSelection.selected = false;
            },
            render = function () {
                navigation.canvas.context.clearRect(0, 0, navigation.canvas.width, navigation.canvas.height);
                processStates(navigation.canvas.context, navigation.states, navigation.font);
                processTransitions(navigation.canvas.context, navigation.transitions, navigation.font);
            },
            processStates = function (context, states, font) {
                for (var i = 0; i < states.length; i++) {
                    var state = states[i];
                    context.save();
                    context.fillStyle = '#fff';
                    if (state.selected)
                        context.fillStyle = '#e6f5e6';
                    context.shadowOffsetX = 2;
                    context.shadowOffsetY = 2;
                    context.shadowBlur = 10;
                    context.shadowColor = '#999';
                    context.beginPath();
                    context.rect(state.x, state.y, state.w, state.h);
                    context.fill();
                    context.restore();
                    context.stroke();
                    context.font = 'bold 12px ' + font;
                    context.textAlign = 'center';
                    context.fillText(state.key, state.x + state.w / 2, state.y + 30, state.w - 2);
                    context.textAlign = 'left';
                    context.font = '10px ' + font;
                    if (state.previous)
                        context.fillText('previous', state.x + 5, state.y + 12);
                    context.textAlign = 'right';
                    if (state.current)
                        context.fillText('current', state.x + state.w - 5, state.y + 12);
                    if (state.back > 0)
                        context.fillText('back ' + state.back, state.x + state.w - 5, state.y + 12);
                }
            },
            processTransitions = function (context, transitions, font) {
                context.font = 'italic 12px ' + font;
                context.beginPath();
                for (var i = 0; i < transitions.length; i++) {
                    var transition = transitions[i];
                    context.moveTo(transition.x1, transition.y);
                    context.lineTo(transition.x1, transition.y + transition.h);
                    context.lineTo(transition.x2, transition.y + transition.h);
                    context.lineTo(transition.x2, transition.y);
                    context.textAlign = 'center';
                    context.fillText(transition.key, transition.x1 + (transition.x2 - transition.x1) / 2,
                        transition.y + transition.h + 12);
                    context.moveTo(transition.x2 - 5, transition.y + 10);
                    context.lineTo(transition.x2, transition.y);
                    context.lineTo(transition.x2 + 5, transition.y + 10);
                }
                context.stroke();
            };
        pubsub.subscribe('trigger.navigation.shell.subscriptions', wireListeners);
        pubsub.subscribe('trigger.navigation.event.render', render);
    })();

    (function () {
        var init = function () {
            pubsub.publish('trigger.navigation.shell.init');
            pubsub.publish('trigger.navigation.shell.subscriptions');
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
