(function($, window, core) {

   var templateEngine = {
      append      : function (container, template, data) {
         core.check.notNull(container, 'templateEngine', 'append', 'container');
         core.check.notNullOrEmpty(template, 'templateEngine', 'append', 'template');

         $(container).jqoteapp(template, data);
      },
      getHtml     : function (template, data) {
         
         core.check.notNull(template, 'templateEngine', 'getHtml', 'template');

         return $.jqote(template, data);
      },
      getTemplateFunction : function (template) {
         core.check.notNullOrEmpty(template, 'templateEngine', 'getTemplateFunction', 'template');
         
         return $.jqotec(template);
      }
   };

   var templateProvider = {
      _templates  : {},
      getTemplateJs : function(module, name, callback) {
         core.check.notNullOrEmpty(module, 'templateProvider', 'getTemplate', 'module');
         core.check.notNullOrEmpty(name, 'templateProvider', 'getTemplate', 'name');
         
         callback = callback || $.noop;
         
         
      },
      addTemplate : function(name, template) {
         core.check.notNullOrEmpty(name, 'templateProvider', 'addTemplate', 'name');

         this._template[name] = template;
      }
   };

   window.template = {
      engine   : templateEngine,
      provider : templateProvider
   };

   core.templateProvider  = templateProvider;
   core.templateEngine    = templateEngine;

})(jQuery, window, window.core);