using SDL2;
using SDLC_.GameLibrary;

internal class Animation
{
    private List<Sprite> sprites = new List<Sprite>();
    private int currentFrameIndex = 0;

    public Animation(Resources.GameObjects obj, Resources.Animation_State state)
    {
        this.sprites = GetAnimationSprites(obj, state);
    }

    public List<Sprite> GetSprites()
    {
        return this.sprites;
    }

    public IntPtr GetCurrentFrameTexture()
    {
        if (sprites.Count > 0)
        {
            return sprites[currentFrameIndex].texture;
        }

        return IntPtr.Zero;
    }

    public Sprite GetCurrentFrameSprite()
    {
        return sprites[currentFrameIndex];
    }

    public int GetCurrentFrameIndex()
    {
        return currentFrameIndex;
    }

    public void SetCurrentFrameIndex(int index)
    {
        if (index >= 0 && index < sprites.Count)
        {
            currentFrameIndex = index;
        }
    }

    public int GetFrameCount()
    {
        return sprites.Count;
    }

    private List<Sprite> GetAnimationSprites(Resources.GameObjects obj, Resources.Animation_State state)
    {
        List<Sprite> animationSprites = new List<Sprite>();

        switch (obj)
        {
            case Resources.GameObjects.PLAYER:
                switch (state)
                {
                    case Resources.Animation_State.TOP:
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_1));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_2));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_3));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_4));
                        break;
                    case Resources.Animation_State.LEFT:
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_1));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_2));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_3));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_4));
                        break;
                    case Resources.Animation_State.BOTTOM:
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_1));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_2));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_3));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_4));
                        break;
                    case Resources.Animation_State.RIGHT:
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_1));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_2));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_3));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_4));
                        break;
                    case Resources.Animation_State.NULL:
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLEBOTTOM_1));
                        animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLEBOTTOM_2));
                        break;
                }
                break;
        }
        return animationSprites;
    }
}
