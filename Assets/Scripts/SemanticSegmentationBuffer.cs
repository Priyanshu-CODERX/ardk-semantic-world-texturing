using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK;
using Niantic.ARDK.AR;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.Awareness;
using Niantic.ARDK.AR.Awareness.Semantics;
using UnityEngine.UI;

public class SemanticSegmentationBuffer : MonoBehaviour
{
    //pass in our semantic manager
    public ARSemanticSegmentationManager _semanticManager;

    public RawImage _overlayImage;
    public Texture2D _semanticTexture;

    void Start()
    {
        //add a callback for catching the updated semantic buffer
        _semanticManager.SemanticBufferUpdated += OnSemanticsBufferUpdated;
    }

    //will be called when there is a new buffer
    private void OnSemanticsBufferUpdated(ContextAwarenessStreamUpdatedArgs<ISemanticBuffer> args)
    {
        //get the buffer that has been surfaced.
        ISemanticBuffer semanticBuffer = args.Sender.AwarenessBuffer;
        //ask for a mask of the sky channel
        int channel = semanticBuffer.GetChannelIndex("sky");

        _semanticManager.SemanticBufferProcessor.CopyToAlignedTextureARGB32
        (
            texture: ref _semanticTexture,
            channel: channel,
            orientation: Screen.orientation
        );

        _overlayImage.texture = _semanticTexture;
    }


}
