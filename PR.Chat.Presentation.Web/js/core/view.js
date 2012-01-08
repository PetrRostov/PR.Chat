var view = function view(options) {
   this.options = $.extend({ }, this.options, options || { });
   this._init();
};
view.prototype = {
   $container  : null,
   canReloaded : true,
   events      : {
      PREINIT        : 'preinit',
      DATA_PRELOAD   : 'dataPreload',
      DATA_LOADED    : 'dataLoaded',
      RENDER         : 'render'
   },
   options  : {
      templateProvider : null
   },
   _init : function () {
      
   },
   dispose : function () {
      
   }
};
