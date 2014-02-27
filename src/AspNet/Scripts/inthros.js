$(document).ready(function () {
    var clearTextSelection = function () {
        if (window.getSelection) {
            window.getSelection().removeAllRanges();
        } else if (document.selection) {
            document.selection.empty();
        }
    };

    var getActivityExpander = function (activity) {
        var header = activity.children(':first-child');

        return header.find('.icon');
    };

    var toggleActivityCollapsedState = function (activity) {
        activity.toggleClass('ww-activity-collapsed');

        var expander = getActivityExpander(activity);

        if (activity.hasClass('ww-activity-collapsed')) {
            expander.children().removeClass('fa-angle-double-up');
            expander.children().addClass('fa-angle-double-down');
        } else {
            expander.children().removeClass('fa-angle-double-down');
            expander.children().addClass('fa-angle-double-up');
        }
    };

    var activities = $('.ww-activity');

    activities.click(function (e) {
        var activity = $(this);

        activities.removeClass('ww-activity-selected');
        activity.addClass('ww-activity-selected');

        e.stopPropagation();
    });

    activities.each(function () {
        var activity = $(this);

        var isCollapsible = activity.is('[data-collapsible]');

        if (isCollapsible) {
            var expander = getActivityExpander(activity);

            expander.click(function (e) {
                toggleActivityCollapsedState(activity);

                e.stopPropagation();
            });
        }

        activity.dblclick(function (e) {
            // Prevent text selection when double clicking activities
            clearTextSelection();

            if (isCollapsible) {
                toggleActivityCollapsedState(activity);
            }

            e.stopPropagation();
        });
    });
});