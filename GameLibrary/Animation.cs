using SDL2;
using SDLC_.GameLibrary;

// Classe que representa uma animação no jogo.
internal class Animation
{
    private List<Sprite> sprites = new List<Sprite>();
    private int currentFrameIndex = 0;

    // Construtor que inicializa uma animação com base no tipo de objeto e estado.
    public Animation(Resources.GameObjects obj, Resources.Animation_State state, General.DIRECTION direction)
    {
        this.sprites = GetAnimationSprites(obj, state, direction);
    }



    // Método para obter a lista de sprites da animação.
    public List<Sprite> GetSprites()
    {
        return this.sprites;
    }

    // Método para obter a textura do quadro atual da animação.
    public IntPtr GetCurrentFrameTexture()
    {
        if (sprites.Count > 0)
        {
            return sprites[currentFrameIndex].texture;
        }

        return IntPtr.Zero;
    }

    // Método para obter o sprite do quadro atual da animação.
    public Sprite GetCurrentFrameSprite()
    {
        return sprites[currentFrameIndex];
    }

    // Método para obter o índice do quadro atual da animação.
    public int GetCurrentFrameIndex()
    {
        return currentFrameIndex;
    }

    // Método para definir o índice do quadro atual da animação.
    public void SetCurrentFrameIndex(int index)
    {
        if (index >= 0 && index < sprites.Count)
        {
            currentFrameIndex = index;
        }
    }

    // Método para obter a contagem total de quadros na animação.
    public int GetFrameCount()
    {
        return sprites.Count;
    }

    // Método privado para obter os sprites da animação com base no tipo de objeto e estado.
    private List<Sprite> GetAnimationSprites(Resources.GameObjects obj, Resources.Animation_State state, General.DIRECTION direction)
    {
        List<Sprite> animationSprites = new List<Sprite>();

        switch (obj)
        {
            case Resources.GameObjects.PLAYER:
                switch (direction)
                {
                    case General.DIRECTION.TOP:
                        switch (state)
                        {
                            case Resources.Animation_State.IDLE:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLETOP_1));
                                break;
                            case Resources.Animation_State.WALK:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_2));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_3));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_TOP_4));
                                break;
                        }
                        break;
                    case General.DIRECTION.LEFT:
                        switch (state)
                        {
                            case Resources.Animation_State.IDLE:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLELEFT_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLELEFT_2));
                                break;
                            case Resources.Animation_State.WALK:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_2));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_3));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_LEFT_4));
                                break;
                        }
                        break;
                    case General.DIRECTION.BOTTOM:
                        switch (state)
                        {
                            case Resources.Animation_State.IDLE:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLEBOTTOM_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLEBOTTOM_2));
                                break;
                            case Resources.Animation_State.WALK:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_2));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_3));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_BOTTOM_4));
                                break;
                        }
                        break;
                    case General.DIRECTION.RIGHT:
                        switch (state)
                        {
                            case Resources.Animation_State.IDLE:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLERIGHT_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_IDLERIGHT_2));
                                break;
                            case Resources.Animation_State.WALK:
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_1));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_2));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_3));
                                animationSprites.Add(new Sprite(Resources.Images_path.GOKU_RIGHT_4));
                                break;
                        }
                        break;
                }
                break;
            case Resources.GameObjects.APPLE:
                animationSprites.Add(new Sprite(Resources.Images_path.FRUITS_APPLE_1));
                break;
            default:
                animationSprites.Add(new Sprite(Resources.Images_path.OBJECT_UNDEFINED_1));
                break;
        }
        return animationSprites;
    }
}
