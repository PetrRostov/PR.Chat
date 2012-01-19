(function ($, core, check) {

    var eventManager = {
        $div: $('<div/>'),
        on: function (event, eventHandler) {
            check.notNullOrEmpty(event, 'eventManager', 'on', 'event');

            this.$div.bind(event, eventHandler);
        },
        unbind: function (event, eventHandler) {
            this.$div.unbind(event, eventHandler);
        },
        trigger: function (event, data) {
            this.$div.trigger(event, data);
        }
    };
    eventManager.bind = eventManager.on;

    core.eventManager = eventManager;

})(jQuery, window.core, window.core.check);