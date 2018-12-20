//
//  MultiHaptic.m
//  Unity-iPhone
//
//  Created by tangz on 2018/9/6.
//
extern "C" {
    
void _goodGuide() {
        Class SKSRC = NSClassFromString(@"SKStoreReviewController");
        if (SKSRC) {
            [SKSRC performSelector:@selector(requestReview)];
        }
    }
}
