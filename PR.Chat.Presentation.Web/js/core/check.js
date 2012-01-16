(function($, window, undefined) {

   window.core.check = {
      notNull : function(param, clazz, procedure, paramName) {
         if (param === undefined || param === null)
            throw 'Parameter ' + paramName + ' should not be null or undefined in ' + clazz + '.' + procedure;
      },
      notNullOrEmpty : function(param, clazz, procedure, paramName) {
         this.notNull(param, paramName);
         
         if (param === '')
            throw 'Parameter ' + paramName + ' should not be empty string in ' + clazz + '.' + procedure;
      }      
   };
   
})(jQuery, window, undefined);