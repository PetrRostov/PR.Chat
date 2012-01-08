(function($, window) {

var viewManager = {
   activeViews       : {},
   currentRoute      : null,
   currentView       : null,
   routes            : [],
   init              : function (routes) {
      this._initRoutes(routes);
      this._initEvents();
      
   },
   _initRoutes    : function (routes) {
      this.routes = $.isArray(routes) ? routes : [routes];
   },
   _initEvents    : function () {
      
   },
   _appendView : function () {
      
   }
};
   
   
   
})(jQuery, window);