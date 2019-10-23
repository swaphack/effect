#include "UnityAppController.h"

extern "C"
{
    void getSafeAreaInsets(int* left, int* right, int* top, int* bottom)
    {
        UIView* view = (UIView*)GetAppController().unityView;
        UIEdgeInsets insets = UIEdgeInsetsMake(0, 0, 0, 0);
        if (@available(iOS 11.0, tvOS 11.0, *))
            insets = [view safeAreaInsets];
        
        *left = insets.left;
        *right = insets.right;
        *top = insets.top;
        *bottom = insets.bottom;
    }
}

