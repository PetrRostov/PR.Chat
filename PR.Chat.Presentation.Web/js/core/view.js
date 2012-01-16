var view = function view(name, options) {
   this.name      = name;
   this.options   = $.extend({ }, this.options, options || { });
   this._init();
};
view.prototype = {
   name        : null,
   $container  : null,
   parent      : null,
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
